import { Component } from '@angular/core';

@Component({
  selector: 'app-usb-test',
  templateUrl: './usb-test.component.html',
  styleUrls: ['./usb-test.component.css'],
})
export class UsbTestComponent {
  getDeviceList() {
    (navigator as any).usb.getDevices().then((devices: any) => {
      devices.forEach((device: any) => {
        console.log(device.productName); // "Arduino Micro"
        console.log(device.manufacturerName); // "Arduino LLC"
      });
    });
  }
}
