import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {
    makes: any[];
    models: any[];
    vehicle: any = {};
    features: any[];

    constructor(
        private vehicleService: VehicleService){
    }

    ngOnInit(){
        this.vehicleService.getMakes().subscribe(makes => {
            this.makes = makes
            console.log("MAKES", this.makes);
        });

        this.vehicleService.getFeatures().subscribe(features => 
            this.features = features);
    }

    onMakeChange() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
        this.models = selectedMake ? selectedMake.models : [];
        //a better way to do this when using large data sets is to make a seperate call to the back end here instead of downloading all of the models 
        //(all models come along with the make, we could change that structure in our domain model)
    }
}



