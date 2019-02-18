import { ITransaction } from './Transaction';

export interface IAccount {
  id: number;
  name: string;
  description: string;
  percentage: number;
  isPrimary: boolean;
  onHold: boolean;
  totalSavings: number;
  transactions: ITransaction[];
}
