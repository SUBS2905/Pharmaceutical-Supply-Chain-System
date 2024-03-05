export default class User {
  userID: string;
  role: string;
  email: string;
  password: string;
  mfaData: {
    otpSecretKey: string;
    lastOTPGeneration: Date;
    deliveryEmail: string;
    recoveryCodes: string[];
    attemptCounter: number;
  };
}
