export interface ITransaction {
  id: number;
  amount: number;
  transactionType: number;
  createDateTime: Date;
  originalAmount: number;
}
