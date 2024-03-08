import Order from './Order';

export default class Shipment {
  shipperID: string;
  shipperName: string;
  trackingNumber: string;
  currentLocation: string;
  orders: Order[];
  estimatedDelivery: Date;
}

export class AddShipment {
  shipperID: string;
  shipperName: string;
  trackingNumber: string;
  currentLocation: string;
  orderIds: string[];
  estimatedDelivery: Date;
}
