<template>
    <div>
        <b-row >
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <b-row>
                                <b-col>
                                    <h4 class="m-0">Warehouse Report</h4>
                                </b-col>
                                <b-col>
                                    <b-form-input v-model="search" placeholder="Search Supplier || BPO Number"/>
                                </b-col>
                                <b-col>
                                    <b-col class="text-right">
                                        <b-button class="ml-3" variant="outline-primary" size="sm" squared >
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
                                :items="filterSearch"
                                :fields="cargoTable.tableColumns"
                                :busy="cargoTable.isLoading"
                                :per-page="cargoTable.resultsPerPage"
                                :total-rows="cargoRows"
                                @row-clicked="openCargoModal"
                                :tbody-tr-class="rowClass"
                                :current-page="cargoTable.currentPage"
                                id="cargoTable"
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
                                        <b-button @click="openCargoModal(data.item, data.index)" size="sm" class="btn-icon">
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
        <div v-if="selectedCargo">
            <b-modal id="cargo" size="lg" hide-footer hide-header class="text-center" title="Edit Cargo Entry" >
                <b-row>
                    <b-col>
                        <b-tabs fill>
                            <b-tab title="Cargo Overview" title-item-class="tabItem">
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
                                        <label>{{selectedCargo.supplier}}</label>
                                    </b-col>

                                    <b-col>
                                        <label><span class="font-weight-bold">BPO Number:</span> </label>
                                        <label>{{selectedCargo.bpoNumber}}</label>
                                    </b-col>

                                    <b-col>
                                        <label><span class="font-weight-bold">Commercial Invoice #:</span> </label>
                                        <label>{{selectedCargo.commercialInvoiceNumber}}</label>
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
                                        <label>
                                            Hazardous?
                                            <span>
                                                ( <font-awesome-icon class="text-danger" icon="fa-biohazard" /> )
                                            </span>
                                        </label>
                                        <label class="label-good" v-if="selectedCargo.hazardous"><font-awesome-icon icon="fa-check" /></label>
                                        <label class="label-bad" v-if="!selectedCargo.hazardous"><font-awesome-icon icon="fa-times" /></label>
                                    </b-col>
                                    <b-col class="text-center">
                                        <label>
                                            Commercial Invoice Received?
                                            <span>
                                                ( <font-awesome-icon icon="fa-file-invoice-dollar" /> )
                                            </span>
                                        </label>
                                        <label class="label-good" v-if="selectedCargo.commercialInvoiceReceived">
                                            <font-awesome-icon icon="fa-check" />
                                        </label>
                                        <label class="label-bad" v-if="!selectedCargo.commercialInvoiceReceived">
                                            <font-awesome-icon icon="fa-times" />
                                        </label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <label>
                                            Packing List Received?
                                            <span>
                                                ( <font-awesome-icon icon="fa-box" /> )
                                            </span>
                                        </label>
                                        <label class="label-good" v-if="selectedCargo.packingListReceived">
                                            <font-awesome-icon icon="fa-check" />
                                        </label>
                                        <label class="label-bad" v-if="!selectedCargo.packingListReceived">
                                            <font-awesome-icon icon="fa-times" />
                                        </label>
                                    </b-col>
                                    <b-col class="text-center">
                                        <label>
                                            Billed To JKN?
                                            <span>
                                                ( <font-awesome-icon icon="fa-dollar-sign" /> )
                                            </span></label>
                                        <label class="label-good" v-if="selectedCargo.billedToJkn">
                                            <font-awesome-icon icon="fa-check" />
                                        </label>
                                        <label class="label-bad" v-if="!selectedCargo.billedToJkn">
                                            <font-awesome-icon icon="fa-times" />
                                        </label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <label>Atrax Invoice Number</label>
                                        <label>{{selectedCargo.atraxInvoiceNumber}}</label>
                                    </b-col>
                                    <b-col>
                                        <label>Atrax Invoice Date</label>
                                        <label>{{selectedCargo.atraxInvoiceDate | dateFilter}}</label>
                                    </b-col>
                                </b-row>
                            </b-tab >
                            <b-tab title="Package Details" title-item-class="tabItem">
                                <div>
                                    <div class="mt-2">
                                        <div v-for="item in selectedCargo.packageModels" :key="item.index" class="w-100 my-2">
                                            <div v-if="item.isActive" :key="packageKey">
                                                <b-row >
                                                    <b-col>
                                                        <div class="d-flex justify-content-between align-items-center">
                                                            <label class="text-primary font-weight-bold mb-4 text-uppercase">{{item.description}}</label>

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
                                
<!--                                <div v-if="deleteSelected && !addSelected && ! editSelected">
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
                                                        <b-button variant="outline-red" squared class="ml-2">Cancel</b-button>
                                                        <b-button variant="red" squared class="ml-2">Delete</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-col>
                                </div>-->
<!--                                <div v-if="addSelected" >
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
                                                <b-button variant="primary" squared >
                                                    <font-awesome-icon icon="fa-plus" ></font-awesome-icon>
                                                </b-button>
                                            </b-col>
                                        </b-row>
                                        <hr class="mx-3">
&lt;!&ndash;                                        <b-row>
                                            <b-table
                                                striped hover
                                                @row-clicked="removePackage"
                                                :items="packageAdd.dataSource"
                                                :fields="packageAdd.tableColumns"
                                                :busy="packageAdd.isLoading"
                                                :current-page="cargoTable.currentPage"
                                            >
                                                <template #cell(actions)>
                                                    <b-row align-v="center" align-h="end">
                                                        <b-button size="sm" class="btn-icon">
                                                            <font-awesome-icon icon="fa-trash" />
                                                        </b-button>
                                                    </b-row>
                                                </template>

                                            </b-table>
                                        </b-row>&ndash;&gt;
                                        <hr class="mx-3">
                                        <b-row>
                                            <b-col>
                                                <div class="d-flex justify-content-end">
                                                    <div>
                                                        <b-button variant="outline-red" squared class="ml-2">Cancel</b-button>
                                                    </div>
                                                    <div>
                                                        <b-button variant="primary" squared class="ml-2">Save</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                </div>-->
<!--                                <div v-if="editSelected">
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
                                                        <b-button variant="outline-red" squared class="ml-2">Cancel Edit</b-button>
                                                    </div>
                                                    <div>
                                                        <b-button variant="primary" squared class="ml-2" >Update</b-button>
                                                    </div>
                                                </div>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                </div>-->
                            </b-tab>
                        </b-tabs>
                    </b-col>
                </b-row>
                <hr class="mx-3">
                <b-row>
                    <b-col>
                        <div class="d-flex justify-content-end">
                            <div>
                                <b-button variant="outline-red" squared @click="closeCargoModal" class="ml-2">Cancel</b-button>
                            </div>
                        </div>
                    </b-col>
                </b-row>
            </b-modal>
        </div>
    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
    name: 'userView',
    data: () => ({
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
        packageData: {
            description: null,
            quantity: null,
            weight: null,
            length: null,
            width: null,
            height: null,
            cargoModelCargoId: null,
        },
        storageDays: null,
        storageCost: null,
        chargeWeight: null,
        quantity: null,
        totalWeight: null,
        search: '',
    }),
    beforeCreate() {
    },
    created() {
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
        ...mapActions(["requestCargo"]),
        
        openCargoModal(rowItem) {
            this.$bvModal.show('cargo')
            this.$store.commit('setSelectedCargo', rowItem)
            this.calculateStorageDays()
            this.calculateStorageCost()
            this.calculateTotalQty()
            this.calculateTotalWeight()
        },
        closeCargoModal() {
            this.$bvModal.hide('cargo')
        },
        rowClass(item, type) {
            if (!item || type !== 'row') return
            if (item.hazardous === true) return 'table-danger'
        },
        getCargoList() {
            this.loading = true
            this.cargoTable.isLoading = false
            this.requestCargo()
                .then(response => {
                    this.cargoTable.dataSource = response.data
                    this.loading = false
                    this.cargoTable.isLoading = false
                    console.log("CARGO LIST", response.data)
                })
                .catch(() => {
                    // this.$router.push({name: 'login'})
                    // localStorage.removeItem('jwt')
                    // localStorage.removeItem('user')
                    // window.location.reload()
                })
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
        },
    },
    computed: {
        ...mapState(["selectedCargo"]),
        
        cargoRows() {
            return this.cargoTable.dataSource.length
        },
        filterSearch() {
            return this.cargoTable.dataSource.filter((item) => {
                return item.supplier.toLowerCase().match(this.search.toLowerCase()) || item.bpoNumber.toLowerCase().match(this.search.toLowerCase())
            })
        },
        
    },
}
</script>

<style scoped>
.label-good {
    font-size: 1.5rem;
    color: green;
}
.label-bad {
    font-size: 1.5rem;
    color: red;
}
</style>