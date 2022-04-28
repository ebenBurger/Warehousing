<template>
    <div>
        <Loader v-if="isEnterPage"></Loader>
        <b-row v-if="!isEnterPage">
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <div class="d-flex justify-content-between align-items-center">
                                <h4 class="m-0">Warehouse Report</h4>
                            </div>
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
                    <b-row class="mt-3">
                        <b-col>
                            <b-table
                                sort-icon-left
                                striped hover
                                :items="cargoTable.dataSource"
                                :fields="cargoTable.tableColumns"
                                :busy="cargoTable.isLoading"
                                @row-clicked="openCargoEntry"
                                :tbody-tr-class="rowClass"
                                :current-page="cargoTable.currentPage"
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
                                        <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, kg) => acc + kg.kgCBMConversion, 0)) ? (data.item.packageModels.filter(active => active.isActive === true).reduce((acc, kg) => acc + kg.kgCBMConversion, 0)).toFixed(3) : ''}}</span>
                                    </b-row>
                                </template>

                                <template #cell(volumeSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, vol) => acc + vol.volumeMetric, 0)).toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(chargeableWeightSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, chargeWeight) => acc + chargeWeight.chargeableWeight, 0)).toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(storageDaysCalc)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{((new Date() - new Date(data.item.endDateOfFreeStorage)) / ((1000 * 3600 * 24) - 1)).toFixed()}}</span>
                                    </b-row>
                                </template>

                                <template #cell(storageCostsCalc)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">ZAR {{(((data.item.dollarRate * data.item.packageModels.filter(active => active.isActive === true).reduce((acc, weight) => acc + weight.chargeableWeight, 0))) * ((new Date() - new Date(data.item.endDateOfFreeStorage)) / ((1000 * 3600 * 24) - 1))).toFixed(3)}}</span>
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
                                    :total-rows="rows"
                                    :per-page="cargoTable.resultsPerPage"
                                    aria-controls="contactTable"
                                ></b-pagination>
                            </b-row>

                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <b-modal  id="cargoAddModal" size="xl" hide-footer hide-header-close class="text-center" title="Add Cargo">
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
                                <label>Order/ BPO Number</label>
                                <b-form-input v-model="cargo.orderNumber"></b-form-input>
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
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <label>Hazard</label>
                                <toggle-button :value="false"
                                               v-model="cargo.hazardous"
                                               class="d-flex justify-content-center"
                                />
                            </b-col>
                            <b-col class="text-center">
                                <label>Commercial Invoice Received</label>
                                <toggle-button :value="false"
                                               v-model="cargo.commercialInvoiceReceived"
                                               class="d-flex justify-content-center"
                                />
                            </b-col>
                            <b-col class="text-center">
                                <label>Packing List Received</label>
                                <toggle-button :value="false"
                                               class="justify-content-center d-flex"
                                               v-model="cargo.packingListReceived"

                                />
                            </b-col>
                            <b-col class="text-center">
                                <label>Billded To JKN</label>
                                <toggle-button :value="false"
                                               class="justify-content-center d-flex"
                                               v-model="cargo.billedToJkn"
                                />
                            </b-col>

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
                                    <div class="d-flex justify-content-between align-items-center">
                                        <label class="text-primary font-weight-bold mb-4">Package Details</label>
                                        <b-button variant="primary" squared @click="addPackage">
                                            <font-awesome-icon icon="fa-plus" >Cargo</font-awesome-icon>
                                        </b-button>
                                    </div>
                                </b-col>
                            </b-row>
                            <b-row>
                                <label v-if="packageAdd.length <=1"> You have {{packageAdd.length}} item on the packing list.</label>
                                <label v-if="packageAdd.length >=2"> You have {{packageAdd.length}} items on the packing list.</label>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <label class="text-primary font-weight-bold mb-4">Package Details</label>
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
                                    <label>Weight (kg)</label>
                                    <b-form-input type="number" v-model="packageData.weight"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Length</label>
                                    <b-form-input type="number" v-model="packageData.length"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Width</label>
                                    <b-form-input type="number" v-model="packageData.width"></b-form-input>
                                </b-col>
                                <b-col>
                                    <label>Height</label>
                                    <b-form-input type="number" v-model="packageData.height"></b-form-input>
                                </b-col>
                            </b-row>
                            <hr class="mx-3">
                            <b-row>
                                <b-table striped hover :items="packageAdd"></b-table>
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
                           <p>Have you added all the packages to this cargo?</p>
                       </b-col>
                       <b-col v-if="packageAdd.length <= 1">
                           You single Item on the packing list 
                       </b-col>
                       <b-col v-if="packageAdd.length >= 2">
                           You have a total of {{packageAdd.length}} Items
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
            <b-modal id="cargoEdit" size="lg" hide-footer hide-header class="text-center" title="Edit Cargo Entry">
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
                                        <b-col>
                                            <label><span class="font-weight-bold">Supplier:</span> </label>
                                            <label>{{selectedCargo.supplier ? selectedCargo.supplier : "Supplier not selected"}}</label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Date Collected:</span> </label>
                                            <label>{{selectedCargo.dateCollected | dateFilter}}</label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Order Number:</span> </label>
                                            <label>{{selectedCargo.orderNumber}}</label>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label><span class="font-weight-bold">End Date of free Storge</span></label>
                                            <label>{{selectedCargo.endDateOfFreeStorage | dateFilter}}</label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Number of storage days</span></label>
                                            <label>{{this.storageDays}}</label>
                                        </b-col>
                                        <b-col>
                                            <label><span class="font-weight-bold">Storage cost</span> </label>
                                            <label>ZAR {{this.storageCost}}</label>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col>
                                            <label class="font-weight-bold">Total Charged Weight</label>
                                            <label>{{this.chargeWeight}}</label>
                                        </b-col>
                                        <b-col>
                                            <label class="font-weight-bold">Total Quantity</label>
                                            <label>{{this.totalQty}}</label>
                                        </b-col>
                                        <b-col>
                                            <label class="font-weight-bold">Total Weight</label>
                                            <label>{{this.totalWeight}} Kg</label>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col class="text-center">
                                            <label>Hazard <span>( <font-awesome-icon class="text-danger" icon="fa-biohazard" /> )</span></label>
                                            <toggle-button :value="false"
                                                           v-model="selectedCargo.hazardous"
                                                           class="d-flex justify-content-center"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Commercial Invoice Received </label>
                                            <span>( <font-awesome-icon icon="fa-file-invoice-dollar" /> )</span>
                                            <toggle-button :value="false"
                                                           v-model="selectedCargo.commercialInvoiceReceived"
                                                           class="d-flex justify-content-center"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Packing List Received <span>( <font-awesome-icon icon="fa-box" /> )</span></label>
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.packingListReceived"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>

                                    </b-row>
                                    <b-row>
                                        <b-col class="text-center">
                                            <label>Billed To JKN <span>( <font-awesome-icon icon="fa-dollar-sign" /> )</span></label>
                                            
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.billedToJkn"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Completed</label>
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.isComplete"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row class="justify-content-around">
                                        <b-col cols="4">
                                            <label>Supplier Invoice Number</label>
                                            <b-form-input v-model="selectedCargo.supplierInvoiceNumber" />
                                        </b-col>
                                        <b-col cols="5">
                                            <label>Supplier Invoice Date</label>
                                            <b-form-datepicker v-model="selectedCargo.supplierInvoiceDate"></b-form-datepicker>
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
                                                <b-button class="mx-1" variant="primary" squared @click="addClicked" size="sm">
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

                                                            <b-button class="mx-1" variant="outline-primary" squared @click="editClicked(item)" size="sm">
                                                                <font-awesome-icon icon="fa-edit" />
                                                            </b-button>
                                                            <b-button class="mx-1" variant="outline-red" squared @click="toggleDeletePackage(item)" size="sm">
                                                                <font-awesome-icon icon="fa-trash" />
                                                            </b-button>
                                                        </div>
                                                    </b-col>
                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Quantity:</label>
                                                        <h3 class="text-center">{{item.quantity}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Weight (kg):</label>
                                                        <h3 class="text-center">{{item.weight}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Length:</label>
                                                        <h3 class="text-center">{{item.length}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Width:</label>
                                                        <h3 class="text-center">{{item.width}}</h3>
                                                    </b-col>
                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Height:</label>
                                                        <h3 class="text-center">{{item.height}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">CBM Conv.:</label>
                                                        <h3 class="text-center">{{item.kgCBMConversion ? item.kgCBMConversion.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Volume:</label>
                                                        <h3 class="text-center">{{item.volumeCbm ? item.volumeCbm.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Chargable Weight:</label>
                                                        <h3 class="text-center">{{item.chargeableWeight ? item.chargeableWeight.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    
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
                                                <label>Are you sure you want to delete the {{itemSelectedForDelete.description}}</label>
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
                                                    <b-button variant="primary" squared @click="addPackage">
                                                        <font-awesome-icon icon="fa-plus" ></font-awesome-icon>
                                                    </b-button>
                                                </div>
                                            </b-col>
                                        </b-row>
                                        <b-row>
                                            <label v-if="packageAdd.length <=1"> You have {{packageAdd.length}} item on the packing list.</label>
                                            <label v-if="packageAdd.length >=2"> You have {{packageAdd.length}} items on the packing list.</label>
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
                                                <label>Weight (kg)</label>
                                                <b-form-input type="number" v-model="packageData.weight"></b-form-input>
                                            </b-col>
                                            
                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Length</label>
                                                <b-form-input type="number" v-model="packageData.length"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Width</label>
                                                <b-form-input type="number" v-model="packageData.width"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Height</label>
                                                <b-form-input type="number" v-model="packageData.height"></b-form-input>
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
                                                <label>Weight (kg)</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.weight"></b-form-input>
                                            </b-col>

                                        </b-row>
                                        <b-row>
                                            <b-col>
                                                <label>Length</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.length"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Width</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.width"></b-form-input>
                                            </b-col>
                                            <b-col>
                                                <label>Height</label>
                                                <b-form-input type="number" v-model="itemSelectedForEdit.height"></b-form-input>
                                            </b-col>
                                        </b-row>
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-end">
                                                    <div>
                                                        <b-button variant="outline-red" squared @click="editClicked" class="ml-2">Cancel</b-button>
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
                                            <label>Transporter Name</label>
                                            <b-form-input v-model="selectedCargo.transporter"/>
                                        </b-col>
                                        <b-col>
                                            <label>Area</label>
                                            <b-form-input v-model="selectedCargo.deliveryArea"/>
                                        </b-col>

                                    </b-row>
                                    <b-row>
                                        <b-col cols="6">
                                            <label>Date to Collect</label>
                                            <b-form-datepicker v-model="selectedCargo.dateOfCollection"/>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label>Transporter Cost</label>
                                            <b-form-input v-model="selectedCargo.transporterCost"/>
                                        </b-col>
                                        <b-col>
                                            <label>Transporter Invoice Number</label>
                                            <b-form-input v-model="selectedCargo.transporterInvoiceNumber"/>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col cols="6">
                                            <label>Transporter Invoice Date</label>
                                            <b-form-datepicker v-model="selectedCargo.transporterInvoiceDate"/>
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
                                            <b-form-select-option v-for="(item, index) in containerList" :key="index" :value="item.containerId">{{item.containerName}}</b-form-select-option>
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
            orderNumber: null,
            supplierInvoiceNumber: null,
            //TODO
            //number of storage days --- this needs to be calculated in UI 
            //storage cost --- this needs to be calculated in UI
            //current date ---- this needs to be populated in UI
            deliveryArea: null,
            transporter: null,
            transporterCost: null,
            transportedInvoiceNumber: null,
            specialRequirements: null,
            deleteReason: null,
            billedToJkn: false,
            commercialInvoiceReceived: false,
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
                // {
                //     label: 'Cargo Ready Place',
                //     key: 'cargoReadyPlace',
                //     sortable: false,
                //     tdClass:'',
                // },
                {
                    label: 'Order Number',
                    key: 'orderNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Container',
                    key: 'container.containerName',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Invoice Number',
                    key: 'supplierInvoiceNumber',
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
                    label: 'Volume',
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
        selectedContainer: [],
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
                    label: 'Weight',
                    key: 'weight',
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
                    label: '',
                    key: 'actions',
                    sortable: false,
                    tdClass: ''
                },
            ]
        },
        packageList: false,
        itemSelectedForDelete: {},
        deleteSelected: false,
        addSelected: false,
        editSelected: false,
        itemSelectedForEdit: [],
        packageKey: 0,
        storageDays: null,
        storageCost: null,
        chargeWeight: null,
        totalQty: null,
        totalWeight: null,
        
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
        },
        openCargoModal() {
            this.$bvModal.show('cargoAddModal')
            
        },
        hideCargoModal() {
            this.$bvModal.hide('cargoAddModal')
            this.cargo = {
                supplier: null,
                dateCollected: null,
                orderNumber: null,
                description: null,
                quantity: null,
                weight: null,
                length: null,
                width: null,
                height: null,
                //cargo ready place
                //end date of free storage
                atraxInvoiceNumber: null,
                // atraxInvoiceDate: '',
                // kg cbm conversion
                //volume cbm
                //volume metric
                //chargeable weight

                //number of storage days --- this needs to be calculated in UI 
                //storage cost --- this needs to be calculated in UI
                // storageInvoiceDate: null,
                //current date ---- this needs to be populated in UI
                // dollarRate: 0.0,
                deliveryArea: null,
                // dateOfCollection: '',
                transporter: null,
                transporterCost: null,
                transportedInvoiceNumber: null,
                // transporterInvoiceDate: null,
                specialRequirements: null,
                deleteReason: null,
                billedToJkn: false,
                commercialInvoiceReceived: false,
                packingListReceived: false,
                hazardous: false,
                isComplete: false,
                isActive: true,
            }
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
                        this.getCargoList()
                    })
            })
           
        },
        saveExtraPackageToDb() {
            //TODO- loop through the array and add to the db
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
                    })
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
            this.$store.commit('setSelectedCargo', this.selectedCargo)
            this.updateCargo()
            .then(() => {
                this.hideCargoEditModal()
                this.getCargoList()
            })
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
                    this.deleteSelected = !this.deleteSelected
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
            console.log("ITEM SELECTED FOR EDIT", this.itemSelectedForEdit)
        },
        togglePackageAdd() {
            this.packageList = !this.packageList
        },
        calculateStorageDays() {
            ((new Date() - new Date(this.selectedCargo.endDateOfFreeStorage)) / ((1000 * 3600 * 24) - 1)).toFixed(0) >= 0 ? this.storageDays = ((new Date() - new Date(this.selectedCargo.endDateOfFreeStorage)) / ((1000 * 3600 * 24) - 1)).toFixed(0) : this.storageDays = 0
        },
        calculateStorageCost() {
            this.chargeWeight = (this.selectedCargo.packageModels
                .filter(active => active.isActive === true)
                .reduce((acc, weight) => acc + weight.chargeableWeight, 0)).toFixed(3)
            this.storageCost = ((this.selectedCargo.dollarRate * (this.selectedCargo.packageModels
                .filter(active => active.isActive === true)
                .reduce((acc, weight) => acc + weight.chargeableWeight, 0))) * ((new Date() - new Date(this.selectedCargo.endDateOfFreeStorage)) / ((1000 * 3600 * 24) - 1))).toFixed(3)
            console.log("StorageCost", this.storageCost)
        },
        calculateTotalQty() {
            this.totalQty = this.selectedCargo.packageModels
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
        }
    },
    computed: {
        ...mapState([
            "selectedCargo",
            "selectedPackage"
        ]),
        rows() {
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
    
    
</style>