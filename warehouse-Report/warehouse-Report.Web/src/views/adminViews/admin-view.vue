<template>
    <div>
        <b-row>
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
                            </b-col>
                        </b-col>
                    </b-row>
                    <b-row class="mt-3">
                        <b-col>
                            <b-table
                                striped hover
                                :items="cargoTable.dataSource"
                                :fields="cargoTable.tableColumns"
                                :busy="cargoTable.isLoading"
                                @row-clicked="openCargoEntry"
                                id="vehicleTable"
                                :current-page="cargoTable.currentPage"
                            >
                                <template #table-busy>
                                    <div class="text-center my-2">
                                        <b-spinner style="width: 3rem; height: 3rem;"></b-spinner>
                                    </div>
                                </template>
                                
                                <template #cell(dateCollected)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.dateCollected | dateFilter}}</span>
                                    </b-row>
                                </template>

                                <template #cell(endDateOfFreeStorage)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.endDateOfFreeStorage | dateFilter}}</span>
                                    </b-row>
                                </template>

                                <template #cell(kgCBMConvertion)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.kgCBMConvertion.toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(volume)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.volumeCbm.toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(chargeableWeight)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.chargeableWeight.toFixed(3)}}</span>
                                    </b-row>
                                </template>
                                
                                <template #cell(storageCost)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.storageCost.toFixed(2)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(actions)="row">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openCargoEntry(row.item)" size="sm" class="btn-icon">
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
            <b-row class="align-items-center m-0 ">
                <b-col class="d-flex justify-content-center">
                    <B-form class="w-100">
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
                                <b-form-input v-model="cargo.dollarRate" placeholder="Normal rate is at 0.55"></b-form-input>
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
                                               :labels="{checked: 'Yes'}"/>
                            </b-col>
                            <b-col class="text-center">
                                <label>Commercial Invoice Received</label>
                                <toggle-button :value="false"
                                               v-model="cargo.commercialInvoiceReceived"
                                               class="d-flex justify-content-center"
                                               :labels="{checked: 'Yes'}"/>
                            </b-col>
                            <b-col class="text-center">
                                <label>Packing List Received</label>
                                <toggle-button :value="false"
                                               class="justify-content-center d-flex"
                                               v-model="cargo.packingListReceived"
                                               :labels="{checked: 'Yes'}"/>
                            </b-col>
                            <b-col class="text-center">
                                <label>Billded To JKN</label>
                                <toggle-button :value="false"
                                               class="justify-content-center d-flex"
                                               v-model="cargo.billedToJkn"
                                               :labels="{checked: 'Yes'}"/>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Parcel Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Quantity</label>
                                <b-form-input type="number" v-model="cargo.quantity"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Weight (kg)</label>
                                <b-form-input type="number" v-model="cargo.weight"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Length</label>
                                <b-form-input type="number" v-model="cargo.length"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Width</label>
                                <b-form-input type="number" v-model="cargo.width"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Height</label>
                                <b-form-input type="number" v-model="cargo.height"></b-form-input>
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
                                        <b-button variant="primary" squared @click="save" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </B-form>
                </b-col>
            </b-row>
        </b-modal>

        <div v-if="this.selectedCargo">
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
                                                <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>
                                            </div>
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
                                        <b-col cols="4">
                                            <label>Atrax Invoice Number</label>
                                            <b-form-input v-model="selectedCargo.atraxInvoiceNumber" />
                                        </b-col>
                                        <b-col cols="5">
                                            <label>Atrax Invoice Date</label>
                                            <b-form-datepicker v-model="selectedCargo.atraxInvoiceDate"></b-form-datepicker>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label><span class="font-weight-bold">Description:</span> </label>
                                            <label>{{selectedCargo.description}}</label>
                                        </b-col>
                                        <b-col cols="3">
                                            <!--                                TODO- come up with another idea for overview of cargo. -->
<!--                                            <label>Cargo Overview</label>
                                            <b-row>
                                                <b-col cols="4" v-if="selectedCargo.hazardous" >
                                                    <font-awesome-icon class="text-danger h4" icon="fa-biohazard" />
                                                </b-col>
                                                <b-col cols="4" v-if="selectedCargo.commercialInvoiceReceived" >
                                                    <font-awesome-icon icon="fa-file-invoice-dollar" />
                                                </b-col>
                                                <b-col cols="4" v-if="selectedCargo.packingListReceived" >
                                                    <font-awesome-icon icon="fa-list-ol"/> 
                                                </b-col>
                                                <b-col cols="4" v-if="selectedCargo.billedToJkn" >
                                                    <font-awesome-icon icon="fa-receipt" />
                                                </b-col>-->
<!--                                            </b-row>-->
                                        </b-col>
                                        
                                    </b-row>
                                    <b-row>
                                        <b-col class="text-center">
                                            <label>Hazard</label>
                                            <toggle-button :value="false"
                                                           v-model="selectedCargo.hazardous"
                                                           class="d-flex justify-content-center"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Commercial Invoice Received</label>
                                            <toggle-button :value="false"
                                                           v-model="selectedCargo.commercialInvoiceReceived"
                                                           class="d-flex justify-content-center"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        <b-col class="text-center">
                                            <label>Packing List Received</label>
                                            <toggle-button :value="false"
                                                           class="justify-content-center d-flex"
                                                           v-model="selectedCargo.packingListReceived"
                                                           :labels="{checked: 'Yes'}"/>
                                        </b-col>
                                        
                                    </b-row>
                                    <b-row>
                                        <b-col class="text-center">
                                            <label>Billed To JKN</label>
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
                            <b-tab title="Cargo Details" title-item-class="tabItem">
                                <b-form class="w-100">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-between align-items-center">
                                                <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                                                <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>
                                            </div>
                                        </b-col>
                                    </b-row>
                                    <b-row class="m-0 colStyle">
                                        <b-col>
                                            <label class="text-center font-weight-bold">Quantity:</label>
                                            <h3 class="text-center">{{selectedCargo.quantity}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Weight (kg):</label>
                                            <h3 class="text-center">{{selectedCargo.weight}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Length:</label>
                                            <h3 class="text-center">{{selectedCargo.length}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Width:</label>
                                            <h3 class="text-center">{{selectedCargo.width}}</h3>
                                        </b-col>
                                    </b-row>
                                    <b-row class="m-0 colStyle">
                                        <b-col>
                                            <label class="text-center font-weight-bold">Height:</label>
                                            <h3 class="text-center">{{selectedCargo.height}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">CBM Conv.:</label>
                                            <h3 class="text-center">{{selectedCargo.kgCBMConversion ? selectedCargo.kgCBMConversion.toFixed(3) : ''}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Volume:</label>
                                            <h3 class="text-center">{{selectedCargo.volumeCbm ? selectedCargo.volumeCbm.toFixed(3) : ''}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Chargable Weight:</label>
                                            <h3 class="text-center">{{selectedCargo.chargeableWeight ? selectedCargo.chargeableWeight.toFixed(3) : ''}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Storage Days:</label>
                                            <h3 class="text-center">{{selectedCargo.numberOfStorageDays}}</h3>
                                        </b-col>
                                        
                                    </b-row>
                                    <b-row class="m-0 colStyle">
                                        <b-col>
                                            <label class="text-center font-weight-bold">Storage Cost:</label>
                                            <h3 class="text-center">{{selectedCargo.storageCost}}</h3>
                                        </b-col>
                                        <b-col>
                                            <label class="text-center font-weight-bold">Volume Metric</label>
                                            <h3 class="text-center">{{selectedCargo.volumeMetric ? selectedCargo.volumeMetric.toFixed(3) : ''}}</h3>
                                        </b-col>
                                    </b-row>
                                    <!--                                    <hr class="mx-3">
                                                                        <b-row>
                                                                            <b-col>
                                                                                <div class="d-flex justify-content-between align-items-center">
                                                                                    <label class="text-primary font-weight-bold mb-4">Transporter Details</label>
                                                                                </div>
                                                                            </b-col>
                                                                        </b-row>
                                                                        <b-row>
                                                                            <b-col>
                                                                                <label>Area</label>
                                                                                <b-form-input v-model="selectedCargo.diliveryArea"/>
                                                                            </b-col>
                                                                            <b-col>
                                                                                <label>Date to Collect</label>
                                                                                <b-form-datepicker v-model="selectedCargo.dateOfCollection"/>
                                                                            </b-col>
                                                                            <b-col>
                                                                                <label>Transporter Name</label>
                                                                                <b-form-input v-model="selectedCargo.transporter"/>
                                                                            </b-col>
                                                                            <b-col>
                                                                                <label>Transporter Cost</label>
                                                                                <b-form-input v-model="selectedCargo.transporterCost"/>
                                                                            </b-col>
                                                                        </b-row>
                                                                        <b-row>
                                                                            <b-col cols="3">
                                                                                <label>Transporter Invoice Number</label>
                                                                                <b-form-input v-model="selectedCargo.transporterInvoiceNumber"/>
                                                                            </b-col>
                                                                            <b-col cols="3">
                                                                                <label>Transporter Invoice Date</label>
                                                                                <b-form-datepicker v-model="selectedCargo.transporterInvoiceDate"/>
                                                                            </b-col>
                                                                        </b-row>-->
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
                            <b-tab title="Transporter Details" class="tabItem">
                                <b-form class="w-100">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-between align-items-center">
                                                <label class="text-primary font-weight-bold mb-4">Transporter Details</label>
                                                <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>
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
                        </b-tabs>
                    </b-col>
<!--                    <b-col class="d-flex justify-content-center">
                        <B-form class="w-100">
                            <b-row>
                                <b-col>
                                    <div class="d-flex justify-content-between align-items-center">
                                        <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                                        <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>
                                    </div>
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
                                <b-col>
                                    <label>Atrax Invoice Number</label>
                                    <b-form-input v-model="selectedCargo.atraxInvoiceNumber" />
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col cols="6">
                                    <label><span class="font-weight-bold">Description:</span> </label>
                                    <label>{{selectedCargo.description}}</label>
                                </b-col>
                                <b-col cols="3">
                                    &lt;!&ndash;                                TODO- come up with another isdea for overview of cargo. &ndash;&gt;
                                    &lt;!&ndash;                                <label>Cargo Overview</label>
                                                                    <b-row>
                                                                        <b-col cols="4" v-if="selectedCargo.hazardous" >
                                                                            <font-awesome-icon class="text-danger h4" icon="fa-biohazard" />
                                                                        </b-col>
                                                                        <b-col cols="4" v-if="selectedCargo.commercialInvoiceReceived" >
                                                                            <font-awesome-icon icon="fa-file-invoice-dollar" />
                                                                        </b-col>
                                                                        <b-col cols="4" v-if="selectedCargo.packingListReceived" >
                                                                            <font-awesome-icon icon="fa-list-ol"/> 
                                                                        </b-col>
                                                                        <b-col cols="4" v-if="selectedCargo.billedToJkn" >
                                                                            <font-awesome-icon icon="fa-receipt" />
                                                                        </b-col>
                                                                    </b-row>&ndash;&gt;
                                </b-col>
                                <b-col cols="3">
                                    <label>Atrax Invoice Date</label>
                                    <b-form-datepicker v-model="selectedCargo.atraxInvoiceDate"></b-form-datepicker>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <label class="font-weight-bold">Special Requirement</label>
                                    <b-form-input v-model="selectedCargo.specialRequirements" />
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col class="text-center">
                                    <label>Hazard</label>
                                    <toggle-button :value="false"
                                                   v-model="selectedCargo.hazardous"
                                                   class="d-flex justify-content-center"
                                                   :labels="{checked: 'Yes'}"/>
                                </b-col>
                                <b-col class="text-center">
                                    <label>Commercial Invoice Received</label>
                                    <toggle-button :value="false"
                                                   v-model="selectedCargo.commercialInvoiceReceived"
                                                   class="d-flex justify-content-center"
                                                   :labels="{checked: 'Yes'}"/>
                                </b-col>
                                <b-col class="text-center">
                                    <label>Packing List Received</label>
                                    <toggle-button :value="false"
                                                   class="justify-content-center d-flex"
                                                   v-model="selectedCargo.packingListReceived"
                                                   :labels="{checked: 'Yes'}"/>
                                </b-col>
                                <b-col class="text-center">
                                    <label>Billed To JKN</label>
                                    <toggle-button :value="false"
                                                   class="justify-content-center d-flex"
                                                   v-model="selectedCargo.billedToJkn"
                                                   :labels="{checked: 'Yes'}"/>
                                </b-col>
                            </b-row>
                            <hr class="mx-3">
                            <b-row>
                                <b-col>
                                    <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                                </b-col>
                            </b-row>
                            <b-row class="m-0 colStyle">
                                <b-col>
                                    <label class="text-center font-weight-bold">Quantity:</label>
                                    <h3 class="text-center">{{selectedCargo.quantity}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Weight (kg):</label>
                                    <h3 class="text-center">{{selectedCargo.weight}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Length:</label>
                                    <h3 class="text-center">{{selectedCargo.length}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Width:</label>
                                    <h3 class="text-center">{{selectedCargo.width}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Height:</label>
                                    <h3 class="text-center">{{selectedCargo.height}}</h3>
                                </b-col>

                            </b-row>
                            <b-row class="m-0 colStyle">
                                <b-col>
                                    <label class="text-center font-weight-bold">CBM Conv.:</label>
                                    <h3 class="text-center">{{selectedCargo.kgCBMConversion ? selectedCargo.kgCBMConversion.toFixed(3) : ''}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Volume:</label>
                                    <h3 class="text-center">{{selectedCargo.volumeCbm ? selectedCargo.volumeCbm.toFixed(3) : ''}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Chargable Weight:</label>
                                    <h3 class="text-center">{{selectedCargo.chargeableWeight ? selectedCargo.chargeableWeight.toFixed(3) : ''}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Storage Days:</label>
                                    <h3 class="text-center">{{selectedCargo.numberOfStorageDays}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Storage Cost:</label>
                                    <h3 class="text-center">{{selectedCargo.storageCost}}</h3>
                                </b-col>
                                <b-col>
                                    <label class="text-center font-weight-bold">Volume Metric</label>
                                    <h3 class="text-center">{{selectedCargo.volumeMetric ? selectedCargo.volumeMetric.toFixed(3) : ''}}</h3>
                                </b-col>
                            </b-row>
                            <hr class="mx-3">
                            <b-row>
                                <b-col>
                                    <div class="d-flex justify-content-between align-items-center">
                                        <label class="text-primary font-weight-bold mb-4">Transporter Details</label>
                                    </div>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <label>Area</label>
                                    <b-form-input v-model="selectedCargo.diliveryArea"/>
                                </b-col>
                                <b-col>
                                    <label>Date to Collect</label>
                                    <b-form-datepicker v-model="selectedCargo.dateOfCollection"/>
                                </b-col>
                                <b-col>
                                    <label>Transporter Name</label>
                                    <b-form-input v-model="selectedCargo.transporter"/>
                                </b-col>
                                <b-col>
                                    <label>Transporter Cost</label>
                                    <b-form-input v-model="selectedCargo.transporterCost"/>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col cols="3">
                                    <label>Transporter Invoice Number</label>
                                    <b-form-input v-model="selectedCargo.transporterInvoiceNumber"/>
                                </b-col>
                                <b-col cols="3">
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
                        </B-form>
                    </b-col>-->
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
                                        <b-button variant="outline-red" squared @click="toggleDelete" class="ml-2">Cancel</b-button>
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

export default {
    data: () => ({
        cargo: {
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
        },
        cargoTable: {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: false,
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
                    label: 'Cargo Ready Place',
                    key: 'cargoReadyPlace',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Order Number',
                    key: 'orderNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Invoice Number',
                    key: 'atraxInvoiceNumber',
                    sortable: true,
                    tdClass:'',
                },
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
                    label: 'Weight (KG)',
                    key: 'weight',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'KG - CBM Conv.',
                    key: 'kgCBMConversion',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Length',
                    key: 'length',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Width',
                    key: 'width',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Height',
                    key: 'height',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Volume',
                    key: 'volume',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Chargeable Weight',
                    key: 'chargeableWeight',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Storage Days',
                    key: 'numberOfStorageDays',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Storage Cost',
                    key: 'storageCost',
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
        isLoading: false
    }),
    beforeCreate() {
    },
    created() {
        this.getSupplierList()
        this.getCargoList()
    },
    beforeMount() {
    },
    mounted() {
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        ...mapActions(["requestSupplier", "createCargo", "requestCargo", "updateCargo"]),
        ...mapState(["selectedCargo"]),
        openCargoEntry(rowItem) {
            this.isLoading = true
            this.$bvModal.show('cargoEdit')
            this.$store.commit('setSelectedCargo', rowItem)
            console.log("SELECTED CARGO", this.selectedCargo)
            this.isLoading = false
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
        },
        getSupplierList() {
            this.requestSupplier()
                .then(response => {
                    this.suppliers = response.data
                    console.log('SUPPLIER', this.supplier)
                })
        },
        getCargoList() {
            this.requestCargo()
            .then(response => {
                this.cargoTable.dataSource = response.data
            })
        },
        save() {
            this.$store.commit('setCreateCargoRequest', this.cargo)
            this.state = 'loading'
            this.createCargo()
                .then(() => {
                    this.$bvModal.hide('cargoAddModal')
                    this.getCargoList()
                })
                .catch(() => {
                })
        },
        cargoUpdate() {
            this.$store.commit('setSelectedCargo', this.selectedCargo)
            this.updateCargo()
            .then(() => {
                this.hideCargoEditModal()
            })
        },
        toggleDelete() {
            this.isDeleteSelected = !this.isDeleteSelected
            
        },
        deleteCargo() {
            this.selectedCargo.isActive = false
            this.$store.commit('setSelectedCargo', this.selectedCargo)
            this.updateCargo()
                .then(() => {
                    this.hideCargoEditModal()
                    this.getCargoList()
                })
        },
    },
    computed: {
        rows() {
            return this.cargoTable.dataSource.length
        },
        ...mapState([
            "selectedCargo"
        ])
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