import { SaveVehicle } from './../models/vehicle';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

    constructor(private http: Http) {}

    toQueryString(obj:any) {
        var parts = [];
        for (var property in obj) {
            var value = obj[property];
            if (value != null && value != undefined) {
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
            }
        }       
        return parts.join('&');
    }

    getMakes() {
        return this.http.get('/api/makes')
            .map(res => res.json());
    }

    getFeatures() {
        return this.http.get('/api/features')
            .map(res => res.json());
    }

    create(vehicle: any) {
        return this.http.post('/api/vehicles/', vehicle)
            .map(res => res.json());
    }

    getVehicle(id: number) {
        return this.http.get('/api/vehicles/' + id)
            .map(res => res.json());
    }

    getVehicles(filter: any) {
        return this.http.get('api/vehicles/' + '?' + this.toQueryString(filter))
            .map(res=> res.json());
    }

    update(vehicle: SaveVehicle) {
        return this.http.put('/api/vehicles/' + vehicle.id, vehicle)
            .map(res => res.json());
    }

    delete(id: number) {
        return this.http.delete('/api/vehicles/' + id)
            .map(res => res.json());
    }
}