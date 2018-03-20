import { ErrorHandler } from "@angular/core";
import { ToastyService } from "ng2-toasty";
import { Inject, NgZone, isDevMode } from '@angular/core';
import * as Raven from 'raven-js';

export class AppErrorHandler implements ErrorHandler {
    constructor(@Inject(ToastyService) private toastyService: ToastyService, @Inject(NgZone) private ngZone: NgZone){
        //look up zones video on udemy if toast notification doesnt work properly
    }

    handleError(error: any): void {
        if (!isDevMode()){
            Raven.captureException(error.originalError || error)
        }
        else {
            if (typeof(window) !== 'undefined') {
                this.ngZone.run(() => {
                    this.toastyService.error({
                        title: 'Error',
                        msg: 'An unexpected error occurred.',
                        theme: 'bootstrap',
                        showClose: true,
                        timeout: 5000
                    });
                })
            }
        } 
    }
}