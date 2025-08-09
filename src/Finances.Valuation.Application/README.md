What should be this application doing.


Track all debts => What I need to pay monthly
                => What I need to pay extra to pay off erlier and wehn it will be paid off

Track all spendings => Let me insert approximetly all my spendings 

Track all my investments => Let me insert all my investments amounts

Track all my savings => Let me insert all savings I have

Track all my future incomes => Let me insert all my aproximate future incomes

Create moneyflow strategies => Let me prioritize the flow for each row

Overall - show me how much liquidity I have => Spendings + Investments
        - show me progress of debt 
        - show me progress of saving target amount
        - show me when approximately I will hit the target saving amount based on future income and future expenses

How to calculate when I will hit the target saving amount

1) Take the actual money flow strategy
2) Take actual amount in the saving
3) Apply income for the month => Get result for the saving add it to the rest
4) Repeat until goal is hit

It should also calculate how many months I can survive on the savings

Savings Longevity Feature
------------------------
This feature calculates how many months your current savings and investments will last, based on your spendings. 
The calculation is performed by the `SavingsLongevityCalculationService`, which:

- Retrieves all savings and investments amounts.
- Retrieves all spendings and calculates the monthly total (currently only monthly spendings are considered).
- Divides the total available funds (savings + investments) by the monthly spendings.
- Returns the number of months your funds will cover your spendings.

This feature is implemented as a separate module and does not mix with other features.



TODO

Add account name information and grouping strategy configuration based on accounts
Also apply calculation to automatically update the records in the db - savings, spendings etc.


