Below are the instructions for the coding scenario. 
Rules:
1.     The solution should be done as a Windows console application written in C#.

2.     The input should be read from a text file where the filename is given as the first and only parameter to the Windows console application.

3.     The output should be written to a text file in the same directory as the input file.

4.     The output filename should be the original input filename + “.out”.  For example, if the input file was expenses.txt then the output file would be expenses.txt.out.

5.     The solution should follow coding best practices.

6.     Unit tests should be created.

7.     You must provide the full source code of your solution as a zip file.

8.     Your zip package should only include your source files and no temporary files or binaries.

Problem: Splitting the Bill

A number of friends go camping every year at provincial parks.

The group agrees in advance to share expenses equally, but it is not practical to have them share every expense as it occurs. So individuals in the group pay for particular things, like food, drinks, supplies, the camp site, parking, etc. After the camping trip, each person’s expenses are tallied and money is exchanged so that the net cost to each is the same. In the past, this money exchange has been tedious and time consuming. Your job is to compute, from a list of expenses, the amount of money that each person must pay or be paid.

The Input

Standard input will contain the information for several camping trips. The information for each trip consists of a line containing a positive integer, n, the number of peopling participating in the camping trip, followed by n groups of inputs, one for each camping participant.  Each groups consists of a line containing a positive integer, p, the number of receipts/charges for that participant, followed by p lines of input, each containing the amount, in dollars and cents, for each charge by that camping participant.  A single line containing 0 follows the information for the last camping trip.

The Output

For each camping trip, output one line per participant indicating how much he/she must pay or be paid rounded to the nearest cent.  If the participant owes money to the group, then the amount is positive.  If the participant should collect money from the group, then the amount is negative.  Negative amounts should be denoted by enclosing the amount in brackets.  Each trip should be separated by a blank line.

Sample Input
3
2
10.00
20.00
4
15.00
15.01
3.00
3.01
3
5.00
9.00
4.00
2
2
8.00
6.00
2
9.20
6.75
0

Output for Sample Input
($1.99)
($8.01)
$10.01

$0.98
($0.98)


////////////////////

https://autofaccn.readthedocs.io/en/latest/register/parameters.html