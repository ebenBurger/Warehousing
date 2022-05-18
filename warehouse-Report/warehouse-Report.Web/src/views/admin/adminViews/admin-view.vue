<template>
    <div>
        <Loader v-if="isEnterPage"></Loader>
        <b-row v-if="!isEnterPage">
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <b-row>
                                <b-col>
                                    <h4 class="m-0">Warehouse Report</h4>
                                </b-col>
                                <b-col>
                                    <b-form-input v-model="search" placeholder="Search suppliers" @keyup="filterSearch"/>
                                </b-col>
                                <b-col>
                                    <b-col class="text-right">
                                        <b-button variant="outline-primary" size="sm" squared @click="openCargoModal">
                                            <font-awesome-icon icon="fa-plus" class="mr-1" />
                                            Add Entry
                                        </b-button>
                                        <b-button class="ml-3" variant="outline-primary" size="sm" squared @click="openCompleteCargo">
                                            <font-awesome-icon icon="fa-truck-loading" class="mr-1" />
                                            Completed Cagro
                                        </b-button>
                                    </b-col>
                                </b-col>
                            </b-row>
                            
                            
                        </b-col>
                    </b-row>
                    <b-row class="mt-3">
                        <b-col>
                            <b-table
                                sort-icon-left
                                striped hover
                                :items="cargoTable.dataSource"
                                :fields="cargoTable.tableColumns"
                                :busy="cargoTable.isLoading"
                                :per-page="cargoTable.resultsPerPage"
                                :total-rows="cargoRows"
                                @row-clicked="openCargoEntry"
                                :tbody-tr-class="rowClass"
                                :current-page="cargoTable.currentPage"
                                id="cargoTable"
                                :filter="search"
                                :filter-function="filterSearch"
                            >
                                <template #table-busy>
                                    <div class="text-center my-2">
                                        <b-spinner style="width: 3rem; height: 3rem;"></b-spinner>
                                    </div>
                                </template>

                                <template #cell(dateCollected)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{data.item.dateCollected | dateFilter}}</span>
                                    </b-row>
                                </template>

                                <template #cell(endDateOfFreeStorage)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{data.item.endDateOfFreeStorage | dateFilter}}</span>
                                    </b-row>
                                </template>

                                <template #cell(quantitySum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{data.item.packageModels.filter(active => active.isActive === true).reduce((acc, qty) => acc + qty.quantity, 0)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(icons)="data">
                                    <b-row align-v="center">
                                        <span v-if="data.item.hazardous" class="mr-1">
                                            <font-awesome-icon class="text-danger" icon="fa-biohazard" />
                                        </span>
                                        <span v-if="data.item.packingListReceived" class="mr-1">
                                            <font-awesome-icon icon="fa-box" />
                                        </span>
                                        <span v-if="data.item.commercialInvoiceReceived" class="mr-1">
                                            <font-awesome-icon icon="fa-file-invoice-dollar" />
                                        </span>
                                        <span v-if="data.item.billedToJkn" class="mr-1">
                                            <font-awesome-icon icon="fa-dollar-sign" />
                                        </span>
                                    </b-row>
                                </template>

                                <template #cell(weightSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{data.item.packageModels.filter(active => active.isActive === true).reduce((acc, weight) => acc + weight.weight, 0)}} Kg</span>
                                    </b-row>
                                </template>

                                <template #cell(kgCBMConversionSum)="data">
                                    <b-row align-v="center" >
                                        <span class="mr-auto">
                                        </span>
                                        <span class="mr-auto">
                                            {{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, cbm) => acc + cbm.kgCBMConversion, 0)).toFixed(3)}}
                                        </span>
                                    </b-row>
                                </template>

                                <template #cell(volumeSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{ (data.item.packageModels.filter(active => active.isActive === true).reduce((acc, vol) => acc + vol.volumeCbm, 0)).toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(chargeableWeightSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, Charge) => acc + Charge.chargeableWeight, 0)).toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(storageDaysCalc)="data">
                                    <b-row align-v="center">
                                        <span v-if="data.item.numberOfStorageDays === 0">
                                            <span v-if=" Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)) >= 0" class="mr-auto">
                                            {{
                                                    Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24))
                                                }}
                                        </span>
                                        <span v-if=" Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)) < 0" class="mr-auto">
                                            <font-awesome-icon icon="fa-stopwatch"  class="text-info"/>
                                        </span>
                                        </span>
                                        <span v-if="data.item.numberOfStorageDays > 0">
                                            {{data.item.numberOfStorageDays}}
                                            <font-awesome-icon icon="fa-calendar-days" />
                                        </span>
                                    </b-row>
                                </template>

                                <template #cell(storageCostsCalc)="data">
                                    <b-row align-v="center">
                                        <span v-if="data.item.storageCost === 0">
                                            <span class="mr-auto" v-if="Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)) > 0">
                                            USD
                                            {{
                                                    Math.round(((data.item.packageModels.filter(active => active.isActive === true).reduce((acc, w) => acc + w.chargeableWeight, 0)
                                                            *
                                                            Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)))
                                                        *
                                                        data.item.dollarRate) * 100 + Number.EPSILON) / 100
                                                }}
                                        </span>
                                        <span class="mr-auto" v-if="Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)) < 0">
                                            <font-awesome-icon icon="fa-stopwatch"  class="text-info"/>
                                        </span>
                                        </span>
                                        <span v-if="data.item.storageCost > 0">
                                            Usd
                                            {{Math.round((data.item.storageCost + Number.EPSILON) * 100) / 100}}
                                            <font-awesome-icon icon="fa-server" />
                                        </span>
                                    </b-row>
                                </template>

                                <template #cell(actions)="data">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openCargoEntry(data.item, data.index)" size="sm" class="btn-icon">
                                            <b-icon-chevron-right></b-icon-chevron-right>
                                        </b-button>
                                    </b-row>
                                </template>

                            </b-table>

                            <b-row align-h="center" >
                                <b-pagination
                                    v-model="cargoTable.currentPage"
                                    :total-rows="cargoRows"
                                    :per-page="cargoTable.resultsPerPage"
                                    aria-controls="cargoTable"
                                ></b-pagination>
                            </b-row>

                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <b-modal  id="cargoAddModal" size="xl" hide-footer hide-header-close class="text-center" title="Add Cargo" :no-close-on-backdrop="true">
            <b-row v-if="!isCargoCreated" class="align-items-center m-0 ">
                <b-col class="d-flex justify-content-center">
                    <b-form class="w-100">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="4">
                                <label>Supplier</label>
                                <b-form-select v-model="cargo.supplier">
                                    <b-form-select-option v-for="(item, index) in suppliers" :key="index" :value="item.companyName">{{item.companyName}}</b-form-select-option>
                                </b-form-select>
                            </b-col>
                            <b-col cols="4">
                                <label>Date Collected</label>
                                <b-form-datepicker v-model="cargo.dateCollected"></b-form-datepicker>
                            </b-col>
                            <b-col cols="4">
                                <label>Date Received</label>
                                <b-form-datepicker v-model="cargo.dateReceived"></b-form-datepicker>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="4">
                                <label>Description</label>
                                <b-form-input v-model="cargo.description"></b-form-input>
                            </b-col>
                            <b-col cols="4">
                                <label>USD Rate</label>
                                <b-form-input type="number" v-model="cargo.dollarRate" placeholder="Normal rate is at 0.55"></b-form-input>
                            </b-col>
                            <b-col cols="4">
                                <label>BPO Number</label>
                                <b-form-input v-model="cargo.bpoNumber"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <label>Hazardous?</label>
                                <toggle-button :value="false"
                                               v-model="cargo.hazardous"
                                               class="d-flex justify-content-center"
                                />
                            </b-col>
                            <b-col class="text-center">
                                <label>Commercial Invoice Received?</label>
                                <toggle-button :value="false"
                                               v-model="cargo.commercialInvoiceReceived"
                                               class="d-flex justify-content-center"
                                />
                            </b-col>
                            <b-col class="text-center">
                                <label>Packing List Received?</label>
                                <toggle-button :value="false"
                                               class="justify-content-center d-flex"
                                               v-model="cargo.packingListReceived"

                                />
                            </b-col>
                            <b-col class="text-center">
                                <label>Billed To JKN?</label>
                                <toggle-button :value="false"
                                               class="justify-content-center d-flex"
                                               v-model="cargo.billedToJkn"
                                />
                            </b-col>

                        </b-row>
                        <b-row v-if="cargo.commercialInvoiceReceived">
                            <b-col cols="3"></b-col>
                            <b-col cols="3">
                                <b-form-input v-model="cargo.commercialInvoiceNumber" placeholder="Commercial Invoice Number"></b-form-input> 
                            </b-col>
                            <b-col cols="3"></b-col>
                            <b-col cols="3"></b-col>
                        </b-row>
                        
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Special Requirements</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <b-textarea v-model="cargo.specialRequirements"></b-textarea>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="hideCargoModal" class="ml-2">Cancel</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="primary" squared @click="saveCargo" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </b-form>
                </b-col>
            </b-row>
            <b-row v-if="isCargoCreated" class="align-items-center m-0 ">
                <div v-if="!packageList" >
                    <b-col class="d-flex justify-content-center">
                        <b-form class="w-100">
                            <b-row>
                                <b-col>
                                    <label class="text-primary font-weight-bold mb-4">Shipment Weight</label>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col cols="3">
                                    <label class="font-weight-bold">Total Shipment Weight (kg)</label>
                                    <label class="font-weight-bold text-danger">{{this.packageAdd.dataSource.reduce((acc, item) => acc + parseInt(item.weight), 0)}} Kg</label>
                                </b-col>
                                <b-col cols="3">
                                    <label class="font-weight-bold">Total Shipment Quantity</label>
                                    <label class="font-weight-bold text-danger">{{this.packageAdd.dataSource.reduce((acc, item) => acc + parseInt(item.quantity), 0)}} (Pcs)</label>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <label class="text-primary font-weight-bold mb-4">Shipment Details</label>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <label>Description</label>
                                    <b-form-input v-model="packageData.description"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Quantity</label>
                                    <b-form-input type="number" v-model="packageData.quantity"></b-form-input>
                                </b-col>
                                
                                <b-col>
                                    <label>Length (cm)</label>
                                    <b-form-input type="number" v-model="packageData.length"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Width (cm)</label>
                                    <b-form-input type="number" v-model="packageData.width"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Height (cm)</label>
                                    <b-form-input type="number" v-model="packageData.height"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Weight (kg)</label>
                                    <b-form-input type="number" v-model="packageData.weight"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label></label>
                                    <b-button variant="primary" squared @click="addPackage">
                                        <font-awesome-icon icon="fa-plus" />
                                    </b-button>
                                </b-col>
                            </b-row>
                            <hr class="mx-3">
                            <b-row>
                                <b-table
                                    striped hover
                                    @row-clicked="removePackage"
                                    :items="packageAdd.dataSource"
                                    :fields="packageAdd.tableColumns"
                                    :busy="packageAdd.isLoading"
                                    :current-page="cargoTable.currentPage"
                                >
                                    <template #cell(actions)="row">
                                        <b-row align-v="center" align-h="end">
                                            <b-button @click="removePackage(row)" size="sm" class="btn-icon">
                                                <font-awesome-icon icon="fa-trash" />
                                            </b-button>
                                        </b-row>
                                    </template>

                                </b-table>
<!--                                <b-row align-h="center" >-->
<!--                                    <b-pagination-->
<!--                                        v-model="packageAdd.currentPage"-->
<!--                                        :total-rows="packageAddRows"-->
<!--                                        :per-page="packageAdd.resultsPerPage"-->
<!--                                        aria-controls="packageAddTable"-->
<!--                                    ></b-pagination>-->
<!--                                </b-row>-->
                            </b-row>
                            <hr class="mx-3">
                            <b-row>
                                <b-col>
                                    <div class="d-flex justify-content-end">
                                        <div>
                                            <b-button variant="outline-red" squared @click="hideCargoModal" class="ml-2">Cancel</b-button>
                                        </div>
                                        <div>
                                            <b-button variant="primary" squared @click="confirmPackageList" class="ml-2">Save</b-button>
                                        </div>
                                    </div>
                                </b-col>
                            </b-row>
                        </b-form>
                    </b-col>
                </div>
                <div class="w-100" v-if="packageList">
                   <b-row>
                       <b-col>
                           <p>You are about to add the following to: </p>
                       </b-col>
                       <b-col>
                           <label class="font-weight-bold">Total Shipment Weight (kg)</label>
                           <label class="font-weight-bold text-danger">{{this.packageAdd.dataSource.reduce((acc, item) => acc + parseInt(item.weight), 0)}} Kg</label>
                       </b-col>
                       <b-col>
                           <label class="font-weight-bold">Total Shipment Quantity</label>
                           <label class="font-weight-bold text-danger">{{this.packageAdd.dataSource.reduce((acc, item) => acc + parseInt(item.quantity), 0)}} (Pcs)</label>
                       </b-col>
                   </b-row>
                   <hr class="mx-3">
                   <b-row>
                       <b-col>
                           <div class="d-flex justify-content-end">
                               <div>
                                   <b-button variant="outline-red" squared @click="togglePackageAdd" class="ml-2">Cancel</b-button>
                               </div>
                               <div>
                                   <b-button variant="primary" squared @click="savePackageToDb" class="ml-2">Save Package</b-button>
                               </div>
                           </div>
                       </b-col>
                   </b-row>
               </div>
            </b-row>
        </b-modal>

        <div v-if="selectedCargo" >
            <b-modal id="cargoEdit" size="lg" hide-footer hide-header class="text-center" title="Edit Cargo Entry" :no-close-on-backdrop="true">
                <b-row v-if="!isDeleteSelected">
                    <b-col>
                        <b-tabs fill>
                            <b-tab title="Cargo Overview" title-item-class="tabItem">
                                <b-form class="w-100">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-between align-items-center">
                                                <label class="text-primary font-weight-bold mb-4">Cargo Overview</label>
                                                <b-button variant="outline-red" squared @click="toggleDeleteCargo" size="sm">Delete</b-button>
                                            </div>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col cols="4">
                                            <label><span class="font-weight-bold">Description:</span> </label>
                                            <label>{{selectedCargo.description}} </label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Special Requirements:</span> </label>
                                            <label>{{selectedCargo.specialRequirements}}</label>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col cols="4">
                                            <label><span class="font-weight-bold">Supplier:</span> </label>
                                            <label>{{selectedCargo.supplier}}</label>
                                        </b-col>
                                        
                                        <b-col cols="4">
                                            <label><span class="font-weight-bold">BPO Number:</span> </label>
                                            <label>{{selectedCargo.bpoNumber}}</label>
                                        </b-col>

                                        <b-col v-if="selectedCargo.commercialInvoiceReceived" cols="4">
                                            <label><span class="font-weight-bold">Commercial Invoice Number:</span> </label>
                                            <span>
                                                <b-form-input v-model="selectedCargo.commercialInvoiceNumber"/>
                                            </span>
<!--                                            <span v-show="selectedCargo.commercialInvoiceNumber ? selectedCargo.commercialInvoiceNumber.length > 6 : selectedCargo.commercialInvoiceNumber.length > 12">-->
<!--                                                <label>{{selectedCargo.commercialInvoiceNumber}}</label>-->
<!--                                            </span>-->
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label><span class="font-weight-bold">Date Collected:</span> </label>
                                            <label>{{selectedCargo.dateCollected | dateFilter}}</label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Date Received:</span> </label>
                                            <label>{{selectedCargo.dateReceived | dateFilter}}</label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Date of charge</span></label>
                                            <label>{{selectedCargo.endDateOfFreeStorage | dateFilter}}</label>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        
                                        <b-col cols="4">
                                            <label><span class="font-weight-bold">Number of storage days</span></label>
                                            <label v-if="selectedCargo.numberOfStorageDays === 0">
                                                {{this.storageDays >= 0 ? this.storageDays : "No storage days"}}
                                                <font-awesome-icon icon="fa-tower-broadcast"  class="ml-2 blob"/>
                                            </label>
                                            <label v-if="selectedCargo.numberOfStorageDays > 0">
                                                {{selectedCargo.numberOfStorageDays}}
                                                <font-awesome-icon icon="fa-lock" class="ml-2 text-success" />
                                            </label>
                                        </b-col>
                                        <b-col cols="4">
                                            <label><span class="font-weight-bold">Storage cost</span> </label>
                                            <label v-if="selectedCargo.storageCost === 0">
                                                <span v-if="storageCost > 0"> USD</span>
                                                {{this.storageCost > 0 ? this.storageCost : "No Cost"}}
                                                <font-awesome-icon icon="fa-tower-broadcast"  class="ml-2 blob"/>
                                            </label>
                                            <label v-if="selectedCargo.storageCost > 0">
                                                <span v-if="storageCost >= 0"> USD</span>
                                                {{selectedCargo.storageCost ? selectedCargo.storageCost.toFixed(2) : ''}}
                                                <font-awesome-icon icon="fa-lock" class="ml-2 text-success" />
                                            </label>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        
                                        <b-col>
                                            <label class="font-weight-bold">Total Quantity</label>
                                            <label>{{this.quantity}}</label>
                                        </b-col>
                                        <b-col>
                                            <label class="font-weight-bold">Total Weight</label>
                                            <label>{{this.totalWeight}} Kg</label>
                                        </b-col>
                                        <b-col>
                                            <label class="font-weight-bold">Total Chargeable Weight</label>
                                            <label>{{this.chargeWeight ? this.chargeWeight.toFixed(3) : ''}}</label>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col class="text-center">
                                            <label>Hazardous? <span>( <font-awesome-icon class="text-danger" icon="fa-biohazard" /> )</span></label>
                                            <toggle-button :value="false"
                                                           v-model="selectedCargo.hazardous"
                                                           class="d-flex justify-content-center"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Commercial Invoice Received? </label>
                                            <span>( <font-awesome-icon icon="fa-file-invoice-dollar" /> )</span>
                                            <toggle-button :value="false"
                                                           v-model="selectedCargo.commercialInvoiceReceived"
                                                           class="d-flex justify-content-center"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        

                                    </b-row>
                                    <b-row>
                                        <b-col class="text-center">
                                            <label>Packing List Received? <span>( <font-awesome-icon icon="fa-box" /> )</span></label>
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.packingListReceived"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Billed To JKN? <span>( <font-awesome-icon icon="fa-dollar-sign" /> )</span></label>
                                            
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.billedToJkn"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Completed? </label>
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.isComplete"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row class="justify-content-around">
                                        <b-col cols="4">
                                            <label>Atrax Invoice Number</label>
                                            <b-form-input v-model="selectedCargo.atraxInvoiceNumber" />
                                        </b-col>
                                        <b-col cols="5">
                                            <label>Atrax Invoice Date</label>
                                            <b-form-datepicker v-model="selectedCargo.atraxInvoiceDate"></b-form-datepicker>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-end">
                                                <div>
                                                    <b-button variant="outline-red" squared @click="hideCargoEditModal" class="ml-2">Cancel</b-button>
                                                </div>
                                                <div>
                                                    <b-button variant="primary" squared @click="cargoUpdate" class="ml-2">Update</b-button>
                                                </div>
                                            </div>
                                        </b-col>
                                    </b-row>
                                </b-form>
                            </b-tab >
                            <b-tab title="Package Details" title-item-class="tabItem">
                                <div v-if="!deleteSelected && !addSelected && !editSelected">
                                    <div class="mt-2">
                                        <b-row>
                                            <b-col class="justify-content-end w-100 d-flex">
                                                <b-button v-if="!selectedCargo.containerId" class="mx-1" variant="primary" squared @click="addClicked" size="sm">
                                                    <font-awesome-icon icon="fa-plus" />
                                                </b-button>
                                            </b-col>
                                        </b-row>
                                        <div v-for="item in selectedCargo.packageModels" :key="item.index" class="w-100 my-2">
                                            <div v-if="item.isActive" :key="packageKey">
                                                <b-row >
                                                    <b-col>
                                                        <div class="d-flex justify-content-between align-items-center">
                                                            <label class="text-primary font-weight-bold mb-4 text-uppercase">{{item.description}}</label>

                                                            <div class="d-flex justify-content-between align-items-center" v-if="!selectedCargo.containerId">
                                                                <b-button class="mx-1" variant="outline-primary" squared @click="editClicked(item)" size="sm">
                                                                    <font-awesome-icon icon="fa-edit" />
                                                                </b-button>
                                                                <b-button class="mx-1" variant="outline-red" squared @click="toggleDeletePackage(item)" size="sm">
                                                                    <font-awesome-icon icon="fa-trash" />
                                                                </b-button>
                                                            </div>
                                                        </div>
                                                    </b-col>
                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Quantity:</label>
                                                        <h3 class="text-center">{{item.quantity}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Weight Total (kg):</label>
                                                        <h3 class="text-center">{{item.weight}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Volume CBM:</label>
                                                        <h3 class="text-center">{{item.volumeCbm ? item.volumeCbm.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Chargeable Weight:</label>
                                                        <h3 class="text-center">{{item.chargeableWeight ? item.chargeableWeight.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                    
                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Length: (cm)</label>
                                                        <h3 class="text-center">{{item.length}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Width: (cm)</label>
                                                        <h3 class="text-center">{{item.width}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Height: (cm)</label>
                                                        <h3 class="text-center">{{item.height}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">CBM Conv.:</label>
                                                        <h3 class="text-center">{{item.kgCBMConversion ? item.kgCBMConversion.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                    
                                                    
                                                </b-row>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr v-if="!deleteSelected && !addSelected && !editSelected" class="mx-3">
                                <b-row v-if="!deleteSelected && !addSelected && !editSelected">
                                    <b-col>
                                        <div class="d-flex justify-content-end">
                                            <div>
                                                <b-button variant="outline-red" squared @click="hideCargoEditModal" class="ml-2">Cancel</b-button>
                                            </div>
                                            <div>
                                                <b-button variant="primary" squared @click="cargoUpdate" class="ml-2">Update</b-button>
                                            </div>
                                        </div>
                                    </b-col>
                                </b-row>
                                <div v-if="deleteSelected && !addSelected && ! editSelected">
                                    <b-col>
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-between align-items-center mt-3">
                                                    <label class="text-primary font-weight-bold mb-4">Delete package entry</label>
                                                </div>
                                            </b-col>
                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Are you sure you want to delete the <span class="font-weight-bold">{{itemSelectedForDelete.description}}</span> Cargo</label>
                                            </b-col>
                                        </b-row>
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-end">
                                                    <div>
                                                        <b-button variant="outline-red" squared @click="toggleDeletePackage" class="ml-2">Cancel</b-button>
                                                        <b-button variant="red" squared @click="deletePackage" class="ml-2">Delete</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-col>
                                </div>
                                <div v-if="addSelected" >
                                    <b-form class="w-100">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <label class="text-primary font-weight-bold mb-4">Package Details</label>
                                                    
                                                </div>
                                            </b-col>
                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Description</label>
                                                <b-form-input v-model="packageData.description"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Quantity</label>
                                                <b-form-input type="number" v-model="packageData.quantity"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Weight Total (kg)</label>
                                                <b-form-input type="number" v-model="packageData.weight"></b-form-input>
                                            </b-col>
                                            
                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Length (cm)</label>
                                                <b-form-input type="number" v-model="packageData.length"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Width (cm)</label>
                                                <b-form-input type="number" v-model="packageData.width"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Height (cm)</label>
                                                <b-form-input type="number" v-model="packageData.height"></b-form-input>
                                            </b-col>
                                        </b-row>
                                        <b-row>
                                            <b-col class="text-right mt-2">
                                                <b-button variant="primary" squared @click="addPackage">
                                                    <font-awesome-icon icon="fa-plus" ></font-awesome-icon>
                                                </b-button>
                                            </b-col>
                                        </b-row>
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-table
                                                striped hover
                                                @row-clicked="removePackage"
                                                :items="packageAdd.dataSource"
                                                :fields="packageAdd.tableColumns"
                                                :busy="packageAdd.isLoading"
                                                :current-page="cargoTable.currentPage"
                                            >
                                                <template #cell(actions)="row">
                                                    <b-row align-v="center" align-h="end">
                                                        <b-button @click="removePackage(row)" size="sm" class="btn-icon">
                                                            <font-awesome-icon icon="fa-trash" /> 
                                                        </b-button>
                                                    </b-row>
                                                </template>
                                                
                                            </b-table>
                                        </b-row>
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-end">
                                                    <div>
                                                        <b-button variant="outline-red" squared @click="addClicked" class="ml-2">Cancel</b-button>
                                                    </div>
                                                    <div>
                                                        <b-button variant="primary" squared @click="saveExtraPackageToDb" class="ml-2">Save</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                </div>
                                <div v-if="editSelected">
                                    <b-form class="w-100">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <label class="text-primary font-weight-bold mb-4">Package Details</label>
                                                </div>
                                            </b-col>
                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Description</label>
                                                <b-form-input v-model="itemSelectedForEdit.description"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Quantity</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.quantity"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Weight Total (kg)</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.weight"></b-form-input>
                                            </b-col>

                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Length (cm)</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.length"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Width (cm)</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.width"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Height (cm)</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.height"></b-form-input>
                                            </b-col>
                                        </b-row>
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-end">
                                                    <div>
                                                        <b-button variant="outline-red" squared @click="editClicked" class="ml-2">Cancel Edit</b-button>
                                                    </div>
                                                    <div>
                                                        <b-button variant="primary" squared class="ml-2" @click="updatedEditPackage">Update</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                </div>
                            </b-tab>
                            <b-tab title="Transporter Details" class="tabItem">
                                <b-form class="w-100">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-between align-items-center">
                                                <label class="text-primary font-weight-bold mb-4">Transporter Details</label>
                                                <b-button variant="outline-red" squared @click="toggleDeleteCargo" size="sm">Delete</b-button>
                                            </div>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label>Transporter Invoice Number</label>
                                            <b-form-input v-model="selectedCargo.transporterInvoiceNumber"/>
                                        </b-col>
                                        <b-col>
                                            <label>Transporter Invoice Date</label>
                                            <b-form-datepicker v-model="selectedCargo.transporterInvoiceDate"/>
                                        </b-col>
                                        
                                    </b-row>
                                    <b-row>
                                        <b-col cols="6">
                                            <label>Transporter Cost</label>
                                            <b-form-input v-model="selectedCargo.transporterCost"/>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label>Transporter Name</label>
                                            <b-form-input v-model="selectedCargo.transporter"/>
                                        </b-col>
                                        <b-col>
                                            <label>Area Colleted</label>
                                            <b-form-input v-model="selectedCargo.deliveryArea"/>
                                        </b-col>
                                        
                                    </b-row>
                                    <b-row>
                                        <b-col cols="6">
                                            <label>Date Collect</label>
                                            <b-form-datepicker v-model="selectedCargo.dateOfCollection"/>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-end">
                                                <div>
                                                    <b-button variant="outline-red" squared @click="hideCargoEditModal" class="ml-2">Cancel</b-button>
                                                </div>
                                                <div>
                                                    <b-button variant="primary" squared @click="cargoUpdate" class="ml-2">Update</b-button>
                                                </div>
                                            </div>
                                        </b-col>
                                    </b-row>
                                </b-form>
                            </b-tab>
                            <b-tab title="Container" class="tabItem">
                                <div v-if="selectedCargo.commercialInvoiceReceived && selectedCargo.billedToJkn && selectedCargo.packingListReceived">
                                    <b-form class="w-100" v-if="containerList.length >= 1">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-between align-items-center">
                                                    <label class="text-primary font-weight-bold mb-4">Container Details</label>
                                                </div>
                                            </b-col>
                                        </b-row>
                                        <b-row>
                                            <b-form-select v-model="selectedCargo.containerId">
                                                <b-form-select-option :value="this.null">{{selectedCargo.containerId !== null ? 'Remove From Container' : "Please select a container"}}</b-form-select-option>
                                                <b-form-select-option v-for="(item, index) in containerList" :key="index" :value="item.containerId">{{item.containerNumber}} || {{item.containerType}} || {{item.status}}</b-form-select-option>
                                            </b-form-select>
                                        </b-row>
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-end">
                                                    <div>
                                                        <b-button variant="outline-red" squared @click="hideCargoEditModal" class="ml-2">Cancel</b-button>
                                                    </div>
                                                    <div>
                                                        <b-button variant="primary" squared @click="cargoUpdate" class="ml-2">Update</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                    <div v-if="containerList.length === 0" class="text-center my-5">
                                        <h3>There are no containers available to select from</h3>
                                    </div>
                                </div>
                                <div v-if="!selectedCargo.commercialInvoiceReceived || !selectedCargo.billedToJkn || !selectedCargo.packingListReceived">
                                    <b-row>
                                        <b-col class="text-center my-4">
                                            <p>Cargo is incomplete and not allowed to be loaded in a container</p>
                                            <p>Please, complete all requirements to pack.</p>
                                            <p class="font-weight-bold">Missing:</p>
                                            <p class="font-weight-bold text-danger" v-if="!selectedCargo.commercialInvoiceReceived">Commercial Invoice</p>
                                            <p class="font-weight-bold text-danger" v-if="!selectedCargo.billedToJkn">Billed To JKN</p>
                                            <p class="font-weight-bold text-danger" v-if="!selectedCargo.packingListReceived">Packing List</p>
                                        </b-col>
                                    </b-row>
                                </div>
                            </b-tab>
                        </b-tabs>
                    </b-col>
                </b-row>
                <b-row v-if="isDeleteSelected">
                    <b-col>
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-between align-items-center">
                                    <label class="text-primary font-weight-bold mb-4">Delete Cargo Entry</label>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Please give a reason for deleteing the entry</label>
                                <b-textarea v-model="selectedCargo.deleteReason" />
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="toggleDeleteCargo" class="ml-2">Cancel</b-button>
                                        <b-button variant="red" squared @click="deleteCargo" class="ml-2">Delete</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </b-modal>
        </div>
    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import Loader from "@/components/loader";

export default {
    components: {Loader},
    data: () => ({
        cargo: {
            supplier: null,
            dateCollected: null,
            bpoNumber: null,
            atraxInvoiceNumber: null,
            deliveryArea: null,
            transporter: null,
            transporterCost: null,
            transportedInvoiceNumber: null,
            specialRequirements: null,
            deleteReason: null,
            billedToJkn: false,
            commercialInvoiceReceived: false,
            commercialInvoiceNumber: null,
            packingListReceived: false,
            hazardous: false,
            isComplete: false,
            isActive: true,
            containerId: null,
        },
        packageData: {
            description: null,
            quantity: null,
            weight: null,
            length: null,
            width: null,
            height: null,
            cargoModelCargoId: null,
        },
        cargoTable: {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: true,
            tableColumns: [
                {
                    label: 'Supplier',
                    key: 'supplier',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Date Of Collection',
                    key: 'dateCollected',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Date Charge Start',
                    key: 'endDateOfFreeStorage',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'BPO Number',
                    key: 'bpoNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Container',
                    key: 'container.containerNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Atrax Invoice',
                    key: 'atraxInvoiceNumber',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Quantity',
                    key: 'quantitySum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: '',
                    key: 'icons',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Weight (KG)',
                    key: 'weightSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'KG - CBM Conv.',
                    key: 'kgCBMConversionSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Volume (CBM)',
                    key: 'volumeSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Chargeable Weight',
                    key: 'chargeableWeightSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Storage Days',
                    key: 'storageDaysCalc',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Storage Cost',
                    key: 'storageCostsCalc',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: '',
                    key: 'actions',
                    sortable: false,
                    tdClass: ''
                },
            ]
        },
        suppliers: [],
        isDeleteSelected: false,
        selectedCargoItem: {},
        isEnterPage: true,
        containerList: [],
        isCargoCreated: false,
        cargoCreatedData: 0,
        packageAdd: {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: true,
            tableColumns: [
                {
                    label: 'Description',
                    key: 'description',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Quantity',
                    key: 'quantity',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Length',
                    key: 'length',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Width',
                    key: 'width',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Height',
                    key: 'height',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Weight',
                    key: 'weight',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: '',
                    key: 'actions',
                    sortable: false,
                    tdClass: ''
                },
            ]
        },
        packageList: false,
        itemSelectedForDelete: [],
        deleteSelected: false,
        addSelected: false,
        editSelected: false,
        itemSelectedForEdit: [],
        packageKey: 0,
        storageDays: null,
        storageCost: null,
        chargeWeight: null, 
        quantity: null,
        totalWeight: null,
        search: "",
        filteredItems:[],
        null: null,
        
    }),
    beforeCreate() {
    },
    created() {
        this.getSupplierList()
        this.getCargoList()
        this.getContainerList()
    },
    beforeMount() {
    },
    mounted() {
        setTimeout(() => {
            this.isEnterPage = false
        }, 0)
        // this.filteredItems = this.search
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        ...mapActions([
            "requestSupplier", 
            "createCargo", 
            "requestCargo", 
            "updateCargo", 
            "requestContainer", 
            "createPackage", 
            "updatePackage",
            "updateContainer",
        ]),
        openCargoEntry(rowItem) {
            this.$bvModal.show('cargoEdit')
            this.$store.commit('setSelectedCargo', rowItem)
            console.log("SELECTED CARGO", rowItem)
            this.calculateStorageDays()
            this.calculateStorageCost()
            this.calculateTotalQty()
            this.calculateTotalWeight()
        },
        hideCargoEditModal() {
            this.$bvModal.hide('cargoEdit')
            //TODO clear the pervasive memory on changes not saved.
            this.cargoTable.isLoading = false
            this.editSelected = false
        },
        openCargoModal() {
            this.$bvModal.show('cargoAddModal')
            
        },
        hideCargoModal() {
            this.$bvModal.hide('cargoAddModal')
            this.cargo = {
                supplier: null,
                dateCollected: null,
                dateReceived: null,
                bpoNumber: null,
                description: null,
                quantity: null,
                weight: null,
                length: null,
                width: null,
                height: null,
                atraxInvoiceNumber: null,
                deliveryArea: null,
                transporter: null,
                transporterCost: null,
                transportedInvoiceNumber: null,
                specialRequirements: null,
                deleteReason: null,
                billedToJkn: false,
                commercialInvoiceReceived: false,
                commercialInvoiceNumber: false,
                packingListReceived: false,
                hazardous: false,
                isComplete: false,
                isActive: true,
            }
            this.packageList = false
            this.isCargoCreated = false
            this.getCargoList()
            
        },
        
        getSupplierList() {
            this.requestSupplier()
                .then(response => {
                    this.suppliers = response.data
                })
        },
        getCargoList() {
            this.cargoTable.isLoading = true
            this.requestCargo()
            .then(response => {
                this.cargoTable.dataSource = response.data
                this.cargoTable.isLoading = false
                
            })
            .catch(() => {
                this.$router.push({path: '/'})
                localStorage.removeItem('jwt')
                localStorage.removeItem('user')
            })
        },
        saveCargo() {
            this.$store.commit('setCreateCargoRequest', this.cargo)
            this.state = 'loading'
            this.createCargo()
                .then((res) => {
                    this.cargoCreatedId = res.data.result
                    this.isCargoCreated = true
                    console.log("CARGO CREATE REQUEST 2", res.data.result)
                })
        },
        savePackageToDb() {
            this.isEnterPage = true
            this.packageAdd.dataSource.forEach((item) => {
                const packItem = {}
                packItem.cargoId = this.cargoCreatedId
                packItem.description = item.description
                packItem.height = item.height
                packItem.length = item.length
                packItem.quantity = item.quantity
                packItem.weight = item.weight
                packItem.width = item.width
                packItem.isActive = true
                console.log('PACKAGE ITEM', packItem)
                console.log('SELECTED CARGO', this.selectedCargo)
                this.$store.commit("setCreatePackageRequest", packItem)
                this.createPackage()
                    .then(() => {
                        this.hideCargoModal()
                        this.packageList = false
                        this.isCargoCreated = false
                        this.packageAdd.dataSource.splice(0, this.packageAdd.dataSource.length)
                        this.isEnterPage = false
                    })
            })
            location.reload()
           
        },
        saveExtraPackageToDb() {
            this.isEnterPage = true
            this.packageAdd.dataSource.forEach((item) => {
                const packItem = {}
                packItem.cargoId = this.selectedCargo.cargoId
                packItem.description = item.description
                packItem.height = item.height
                packItem.length = item.length
                packItem.quantity = item.quantity
                packItem.weight = item.weight
                packItem.width = item.width
                packItem.isActive = true
                console.log('PACKAGE ITEM', packItem)
                console.log('SELECTED CARGO', this.selectedCargo)
                this.$store.commit("setCreatePackageRequest", packItem)
                this.createPackage()
                    .then(() => {
                        this.hideCargoEditModal()
                        this.addSelected = !this.addSelected
                        this.getCargoList()
                        this.$store.commit('setSelectedCargo', this.selectedCargo)
                        this.packageAdd.dataSource.splice(0, this.packageAdd.dataSource.length)
                        this.isEnterPage = false
                    })
                location.reload()
            })

        },
        addPackage() {
            const parcel = {}
            parcel.description = this.packageData.description
            parcel.quantity = this.packageData.quantity
            parcel.weight = this.packageData.weight
            parcel.length = this.packageData.length
            parcel.width = this.packageData.width
            parcel.height = this.packageData.height
            parcel.cargoId = this.cargoCreatedId
            parcel.isActive = true
            this.packageAdd.dataSource.push(parcel)
            
            this.packageData.description = null
            this.packageData.quantity = null
            this.packageData.weight = null
            this.packageData.length = null
            this.packageData.width = null
            this.packageData.height = null
            this.packageData.cargoId = null
        },
        removePackage(item) {
            this.packageAdd.dataSource.splice(item.index, 1)
            console.log("REMOVE ITEM", item)
            
        },
        confirmPackageList() {
            this.packageList = true
        },
        cargoUpdate() {
            const updatedCargo = {}
            updatedCargo.supplier = this.selectedCargo.supplier
            updatedCargo.dateCollected = this.selectedCargo.dateCollected
            updatedCargo.dateReceived = this.selectedCargo.dateReceived
            updatedCargo.bpoNumber = this.selectedCargo.bpoNumber
            updatedCargo.atraxInvoiceNumber = this.selectedCargo.atraxInvoiceNumber
            updatedCargo.deliveryArea = this.selectedCargo.deliveryArea
            updatedCargo.transporter = this.selectedCargo.transporter
            updatedCargo.transporterCost = this.selectedCargo.transporterCost
            updatedCargo.transporterInvoiceNumber = this.selectedCargo.transporterInvoiceNumber
            updatedCargo.transporterInvoiceDate = this.selectedCargo.transporterInvoiceDate
            updatedCargo.dateOfCollection = this.selectedCargo.dateOfCollection
            updatedCargo.specialRequirements = this.selectedCargo.specialRequirements
            updatedCargo.deleteReason = this.selectedCargo.deleteReason
            updatedCargo.billedToJkn = this.selectedCargo.billedToJkn
            updatedCargo.commercialInvoiceReceived = this.selectedCargo.commercialInvoiceReceived
            updatedCargo.commercialInvoiceNumber = this.selectedCargo.commercialInvoiceNumber
            updatedCargo.packingListReceived = this.selectedCargo.packingListReceived
            updatedCargo.hazardous = this.selectedCargo.hazardous
            updatedCargo.isComplete = this.selectedCargo.isComplete
            updatedCargo.isActive = this.selectedCargo.isActive
            updatedCargo.containerId = this.selectedCargo.containerId
            updatedCargo.cargoId = this.selectedCargo.cargoId
            updatedCargo.description = this.selectedCargo.description
            updatedCargo.dollarRate = this.selectedCargo.dollarRate
            updatedCargo.cargoReadyPlace = this.selectedCargo.cargoReadyPlace
            updatedCargo.endDateOfFreeStorage = this.selectedCargo.endDateOfFreeStorage
            updatedCargo.dateCreated = this.selectedCargo.dateCreated
            updatedCargo.atraxInvoiceNumber = this.selectedCargo.atraxInvoiceNumber
            updatedCargo.atraxInvoiceDate = this.selectedCargo.atraxInvoiceDate
            updatedCargo.totalChargeableWeight = this.chargeWeight
            
            console.log('CONTAINER ID', this.selectedCargo.containerId)
            this.$store.commit('setSelectedCargo', updatedCargo)
            this.updateCargo()
                .then((resp) => {
                    this.hideCargoEditModal()
                    this.getCargoList()
                    this.getContainerList()
                    
                    console.log('RESPONSE', resp.data)
                })
            
            if (this.selectedCargo.isComplete) {
                this.selectedCargo.isComplete = true

                console.log('CONTAINER ID', this.selectedCargo.containerId)
                this.$store.commit('setSelectedCargo', this.selectedCargo)
                this.updateCargo()
                    .then(() => {
                        this.hideCargoEditModal()
                        this.getCargoList()

                    })
            }
        },
        
        toggleDeleteCargo() {
            this.isDeleteSelected = !this.isDeleteSelected
            
        },
        deleteCargo() {
            this.selectedCargo.isActive = false
            this.selectedCargo.containerId = null
            this.$store.commit('setSelectedCargo', this.selectedCargo)
            this.updateCargo()
                .then(() => {
                    this.hideCargoEditModal()
                    this.getCargoList()
                    this.toggleDeleteCargo()
                })
        },
        toggleDeletePackage(item) {
            this.deleteSelected = !this.deleteSelected
            this.itemSelectedForDelete = item
            console.log("ITEM", item)
        },
        deletePackage() {
            this.itemSelectedForDelete.isActive = false
            this.$store.commit('setSelectedPackage', this.itemSelectedForDelete)
            this.updatePackage()
                .then(() => {
                    this.deleteSelected = false
                    this.hideCargoEditModal()
                    console.log('ITEM SELECTED', this.itemSelectedForDelete)
                })
        },
        updatedEditPackage() {
            const updatedItem = {}
            updatedItem.packageId = this.itemSelectedForEdit.packageId
            updatedItem.cargoId = this.itemSelectedForEdit.cargoId
            updatedItem.description = this.itemSelectedForEdit.description
            updatedItem.quantity = this.itemSelectedForEdit.quantity
            updatedItem.weight = this.itemSelectedForEdit.weight
            updatedItem.length = this.itemSelectedForEdit.length
            updatedItem.width = this.itemSelectedForEdit.width
            updatedItem.height = this.itemSelectedForEdit.height
            updatedItem.isActive = true
            
            
            this.$store.commit('setSelectedPackage', updatedItem)
            this.updatePackage()
                .then(() => {
                    console.log("UPDATED ITEM", updatedItem);
                    this.editSelected = !this.editSelected
                })
        },
        getContainerList() {
            this.requestContainer()
                .then((response) => {
                    this.containerList = response.data 
                    console.log("CONTAINER LIST", this.containerList)
                })
        },
        addClicked() {
            this.addSelected = !this.addSelected
        },
        editClicked(item) {
            this.editSelected = !this.editSelected
            this.itemSelectedForEdit = item
        },
        togglePackageAdd() {
            this.packageList = !this.packageList
        },
        calculateStorageDays() {
            const startDate = new Date(this.selectedCargo.endDateOfFreeStorage)
            const endDate = new Date(Date.now())
            
            const oneDay = 1000 * 60 *60 * 24
            
            const diffInTime = endDate.getTime() - startDate.getTime()
            this.storageDays = Math.round(diffInTime / oneDay)
            
        },
        calculateStorageCost() {
            const startDate = new Date(this.selectedCargo.endDateOfFreeStorage)
            const endDate = new Date(Date.now())

            const oneDay = 1000 * 60 *60 * 24

            const diffInTime = endDate.getTime() - startDate.getTime()
            const days = Math.round(diffInTime / oneDay)
            
            this.chargeWeight = (this.selectedCargo.packageModels
                .filter(active => active.isActive === true)
                .reduce((acc, weight) => acc + weight.chargeableWeight, 0))
            
            this.storageCost = ((this.selectedCargo.dollarRate * this.chargeWeight) * days).toFixed(2)
                
        },
        calculateTotalQty() {
            this.quantity = this.selectedCargo.packageModels
                .filter(active => active.isActive === true)
                .reduce((acc, qty) => acc + qty.quantity, 0)
            
        },
        calculateTotalWeight() {
            this.totalWeight = (this.selectedCargo.packageModels
                .filter(active => active.isActive === true)
                .reduce((acc, weight) => acc + weight.weight, 0)).toFixed(3)
            console.log('TOTAL WEIGHT', this.totalWeight)
        },
        openCompleteCargo() {
            this.$router.push({path: '/completeCargo'})
        },
        rowClass(item, type) {
            if (!item || type !== 'row') return
            if (item.hazardous === true) return 'table-danger'
        },
        
        //TODO filter
        filterSearch(row, search) {
            if (row.supplier !== search) {
                return false
            } else {
                return true
            }
        },
        
    },
    computed: {
        ...mapState([
            "selectedCargo",
            "selectedPackage",
            "selectedContainer"
        ]),
        cargoRows() {
            return this.cargoTable.dataSource.length
        },
        
    },
}
</script>

<style>
    .colStyle > .col {
        border: 1px solid black;
    }
    a:hover {
        color: blue;
    }
    .nav-item > .nav-link {
        color: black;
    }
    .nav-item > .nav-link:hover {
        color: red;
    }
    
    .blob {
        background: transparent;
        border-radius: 50%;
        
        box-shadow: 0 0 0 0 rgba(0, 0, 0, 1);
        transform: scale(1);
        animation: pulse 2s infinite;
    }

    @keyframes pulse {
        0% {
            transform: scale(0.95);
            box-shadow: 0 0 0 0 rgba(0, 0, 0, 0.7);
        }

        70% {
            transform: scale(1);
            box-shadow: 0 0 0 10px rgba(0, 0, 0, 0);
        }

        100% {
            transform: scale(0.95);
            box-shadow: 0 0 0 0 rgba(0, 0, 0, 0);
        }
    }
</style>