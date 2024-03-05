export default class Product {
  productID: string;
  productName: string;
  description: string;
  formulation: string;
  ingredients: string[];
  expirationDate: Date;
  compliance: {
    fda: boolean;
    ema: boolean;
  };
}
