## Features 
- [ ] Debts tracking
- [ ] Spendings tracking
- [ ] Investments tracking
- [ ] Savings tracking
- [ ] Incomes tracking
- [ ] Money flow strategy
- [ ] Savings Longevity Feature
- [ ] Financial wealth steps

Debts tracking
------------------------
What I need to pay monthly

What I need to pay extra to pay off erlier and when it will be paid off

Add account name information

Spendings tracking
------------------------
Track all spendings => Let me insert approximetly all my spendings 
The spending should have mandatory check box - it means it is neccessary to live

Add account name information

Investments tracking
------------------------
Track all my investments => Let me insert all my investments amounts

Add account name information

Savings tracking
------------------------
Track all my savings => Let me insert all savings I have

Add account name information

Incomes tracking
------------------------
Track all my incomes.
It should let me insert future incomes that are forecasted
I should be able to flag income as locked (locked state would mean the income will come 100% or it already did)

Add account name information

Money Flow Strategy Feature
------------------------
Create moneyflow strategies => Let me prioritize the flow for each row

Savings Longevity Feature
------------------------
This feature calculates how many months your current savings and investments will last, based on your spendings. 
The calculation is performed by the `SavingsLongevityCalculationService`, which:

- Retrieves all savings and investments amounts.
- Retrieves all mandatory spendings and calculates the monthly total (currently only monthly spendings are considered).
- Divides the total available funds (savings + investments) by the monthly spendings.
- Returns the number of months your funds will cover your spendings.

Add account name information and grouping strategy configuration based on accounts

Also apply calculation to automatically update the records in the db - savings, spendings etc.

Financial wealth steps
------------------------

It should validate predifined steps it should highlight level of wealth

Net worth metrics tracking
------------------------
It should periodically save actual account balance like savings, debt, investments
Then we should be able to see how it changed in months

Target saving goal date reach
------------------------
How to calculate when I will hit the target saving amount

1) Take the actual money flow strategy
2) Take actual amount in the saving
3) Apply income for the month => Get result for the saving add it to the rest
4) Repeat until goal is hit

Dark/Light mode
------------------------

Support dark and light mode

Localization
------------------------

Support: English, Czech language
