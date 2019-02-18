import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { IAccount } from '../account';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {
  pageTitle: string = 'Accounts';

  private _accountService;

  accounts: IAccount[];
  errorMessage: any;

  constructor(private accountService: AccountService) {
    this._accountService = accountService;
  }

  ngOnInit() {
    this.accountService.getAccounts().subscribe(
      accounts => {
          this.accounts = accounts;
      },
      error => this.errorMessage = <any>error
    );
  }

}
