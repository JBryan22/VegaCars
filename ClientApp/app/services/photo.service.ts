import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PhotoService {

    constructor(private http: Http) { }

    upload(vehicleId: number, photo: any) {
        var formData = new FormData();
        formData.append('file', photo);

        return this.http.post(`/api/vehicles/${vehicleId}/photos`, formData)
            .map(res => res.json());
    }
}