# SQL

SQL can update, delete, create databases, tables, rows.

SQL is case insensitive.

Next statements use `Northwind` database

Each database has lengthy manuals of its SQL and runtime(isolation/transaction/guaranties).

## Query data(get data)

You can get data:

```sql
-- Retrieves all data rows from Customer table
SELECT * FROM Customers;
```

```sql
-- uses ; to execute 2 queries in one call do database
select * from Customers;
select * from Customers;
```

Sql returns data into result table(result set).

We can filter what columns data to retrieve by name(projection):

```sql
select column_name, column_name
from table_name
```

```sql
select CustomerName, City
from Customers
```

Most database allow navigation in resulted set.

```sql
-- allows to  eliminate return of duplicates on database server side before returning result set
select distinct city from customers
```

```sql
select *
from customers
-- allows return only records with specific criteria (filter)
-- where column_name operator value
where country = 'Mexico'
```

```sql
select *
from customers
-- combines several filter criteria
where country= 'Germany'  and (city = 'Berlin' or city = 'Leipzig')
```

Sorting result:

```sql
select *
from customers
-- sort descending by country
order by country DESC,
-- then by name ascending
customername ASC
```

## Adding new records/rows and updates (PUT/PATCH)

```sql
-- will try to insert new row with 3 first columns data, but fail cause there are more columns
insert into customers
values ('a', 'b', 'c')
```

```sql
-- will try to insert new row with only city specified
insert into customers (city)
values ('a')
```

```sql
-- changes existing row
update customers 
-- sets specified column to value - column selector
set country = 'hell'
-- is mostly must to avoid update all items - rows selector
where customerid = 13
```

## Removing records/rows (deletion)

```sql
-- delete rows
delete from customers
-- specified by clause
where city = 'Berlin'
```

## Q$A

Q:How can you remove one record from a table?
A: Use unique id


How to remove all records from a table?
```sql
--delete all rows
delete from customers
```

```sql
--delete all rows
delete * from customers
```

## Security. SQL injection


### `Query` + `or 1 = 1`:

select * from customers where customerid = 1 
+
`or 1 = 1`
=

```sql
select * from customers where customerid = 1 or 1 = 1
```

Q + " or ""=""
---
Q + ` or ""=""

select * from customers where customername = 'Blauer See Delikatessen' and country = 'Germany'
select * from customers where customername = '' or ""="" and country = '' or ""=""


several statements via ;
---
select * from customers;
drop table customers

```
select * from customers; drop table customers
```

Avoid injection - use @ - parameters

```tsql
-- select only several rows
select top 2 * from customers;
select top 2 percent * from customers;
```

## Find. Pattern matching:
```
select * from customers
where city 
-- starts with B and finished with any characters
like 'B%`
```

### Wildcards

%lin    - any start any characters and ends with specified
_erling  - any start one and ends with specified
B_er_ing 
Ber%
Berli_
[bac]%  - starts from any of 3 characters
[!bac]% - not starts


Includes
```sql
select * from customers where city
in ('Paris','Berlin')
-- not in ('Paris','Berlin')
```

Range
```sql
select * from products 
where price 
between 10 and 20
-- not between 10 and 20
```

## Aliases

- Columns

```sql
-- renames columns to better one
SELECT CustomerId as Id, CustomerName as [The Name]
FROM Customers;
```

```sql
-- not works....
select CustomerId, (CustomerName +', '+ City) as GeoName
from customers
```


## Join(combine)

inner (simple) - at least one match in BOTH tables. If there is now match - not row will be resulted
inner join  = join

```sql
select Customers.CustomerId, Customers.CustomerName, Orders.OrderId, Orders.OrderDate
from Orders
inner join Customers
on Orders.CustomerId=Customers.CustomerId
```

left (outer) - all rows are returned from left table, if there is no match in right then NULL returned

```
select *
from left
left join on right
on join.Id = right.LeftIf
```


```
select Customers.CustomerName, Orders.OrderId
-- Customers is left
from Customers 
-- orders is right
left join Orders
on orders.customerid = customers.customerid
order by customers.CustomerName
```


right (outer) - right is fully returned, left is NULL if no match
```
select Orders.OrderId, Customers.CustomerName
from Orders
right join customers
on orders.customerid = customers.customerid
```

full  - return all records, when there is no match on left or right - null returned
```sql
SELECT Customers.CustomerName, Orders.OrderID
FROM Customers
FULL OUTER JOIN Orders
ON Customers.CustomerID=Orders.CustomerID
ORDER BY Customers.CustomerName;
```

###Union (set or bag of 2 tables)

```sql
-- select coulumn of customers
select City from Customers
union -- create set(distinct values) of 2 tables 
-- should have EXACTLY THE SAME COLUMNS
select City from Suppliers
```

```sql
union all -- create bag (with duplicates)
```


## Copying and backup

Creates new table
```sql
select columns
into mytable [in mydatabase]
from  othertable
```

Copy just scheme - create new empty table
```sql
select columns
into mytable [in mydatabase]
from  othertable
where 1 = 2
```

Copy data into existing
```sql
insert into table2 [column name]
select * from table1
```

```sql
insert into customers (CustomerName, Country)
select SupplierName, Country from Suppliers
```

## Creating if new databases and tables


```sql
create database mydb
```

```sql
create table mytable
(id int,
name varchar(100)
)
```

## Q&A:
What parts SQL query consists of (i.e. SELECT then filed list, then FROM then table list and so on)
```sql
SELECT WHAT (all, specific)
FROM (existing table, joined table, view, alias tables, just tables, (SELECT) )
[JOIN ON]
[WHERE (filters of any kind)]
[ORDER BY]
```

Is it possible to put multiple table names in the FROM part of SQL query?
---
Yes, via join or several but joins is defined via where. Or via temporal selected table. SELECT * FROM (SELECT * FROM A).
```
select c.CustomerId, c.CustomerName, o.OrderId, o.OrderDate
-- shorthands access
from Customers as c, Orders as o
-- join
where c.customerid = o.customerid
```


```sql
select c.CustomerId, c.CustomerName, o.OrderId, o.OrderDate
-- shorthands access
from Customers as c, Orders as o
-- join
where c.customerid = o.customerid
```

```sql
select Customers.CustomerId, Customers.CustomerName, Orders.OrderId, Orders.OrderDate
-- 2 tables in from
from Customers, Orders 
-- join
where Customers.customerid = Orders.customerid
```

```sql
-- next query will return Cartesian product
select c.CustomerId, c.CustomerName, o.OrderId, o.OrderDate
from Customers as c, Orders as o
```

Q:What is a query plan? How can you get it from SQL Server?
A:
SQL is declarative. 
Many ways to execute the same statement (different order or algorithms).
Query plan is view of what is executed. 
Starts from data produced by parameterless query parts. Then data is feed into dependant query parts. Algorithms are named and shown.
http://en.wikipedia.org/wiki/Query_plan



Difference between HAVING and WHERE clause
---
HAVING is used with aggregate function, where cannot be used

```
CREATE TABLE Neuron (NeuronId int PRIMARY KEY, NeuronPotential decimal);
```


Q:What is a clustered (and a non-clustered) index?
A: Why clustered index is usually created over a Primary Key? Can it be created over any other field?
Index is logical.
Clustered index reorders real row data in database.
There can be only one Clustered Index to avoid data duplication.
Primary Keys have good properties to be used for Clustered Index. But Clustered Index can be created over other fields.

- What are the advantages and disadvantages of having GUID as a primary key in table?
GUID is 128 bit. Long integer is 64 bit. 
GUID useful to split unique data into several nodes.

### DB dead-locks. How to avoid and now to fix when happen

First transaction locks for update table A then B.
Seconds transaction locks for update B then A.
If First done with A and asks for B. B is still in Second Transaction. First waits.
If Second finishes with B and wants A. But A in scope of First transaction and cannot be changed until finishes.
Use the same order of updates.

### Is it possible to use aggregation functions without GROUP BY in SQL statement? E.g. SELECT a, SUM(b) FROM table
may be yes, but it will aggregate for all rows which is useless.

# Are indexes are always good?
If we update Indexed fields often then Indexes have to be recalculated then having index is bad. 

# What is normalization/denormalization? What are their purposes/goals?
Source code rule do not repeat yourself rule for database. 
- less size of database
- easier to support consistent updates and re factoring
- improves query performance because less size and *


### Given table
Id Name CityId
1  Bob     1
2  Alice   2
3  Jane    2
4  Cat     3

Select all with distinct cities.  There should be only one row resulted with CityId = 2. E.g. skip Jane because she repeats CityId 2. Use SQL. Do not retrieve data to client to filter in programming language.
You should get  like:
1  Bob     1
2  Alice   2
4  Cat     3

A:

SQL Server:

```
drop table persons;

create table persons
(
Id int Identity(1,1) PRIMARY KEY NOT NULL,
Name varchar(255),
CityId int
);

insert into persons values (1,'Bob',1);
insert into persons values (2,'Alice',2);
insert into persons values (3,'Jane',2);
insert into persons values (4,'Cat',3);
```

```
select * from persons
group by cityid -- unite on cityid, if 2 duplicates skip rest rows except 1
```

http://stackoverflow.com/questions/966176/select-distinct-on-one-column


Q: Given table with Id and Name. Calculate number of times Name is in table.
I.e. obtain next table

Name Count
Bob 1
Alice 3
Joe 2

A:

```
drop table persons;

create table persons
(
Id int Identity(1,1) PRIMARY KEY NOT NULL,
Name varchar(255)
)
insert into persons values (1,'Bob');
insert into persons values (1,'Alice');
insert into persons values (1,'Joe');
insert into persons values (1,'Alice');
insert into persons values (1,'Alice');
insert into persons values (1,'Joe');
```

```
SELECT Name, Count(Name) FROM Persons
group by Name
```


Logic in ACID transactional database database(single real time source of truth).
======

Queries should contain only those logic which will prevent unnecessary further queries to database and reduce data downloaded from database and prevent inconsistencies in database.

All calculations on data should be done on client farm.

This restriction may be withdraw if query runs seldom (very very seldom), not as part of second/minute/hourly/daily operation so no effect of logic in database is visible.

This rule does not applies to eventual consistency databases.

## Transaction and isolation

Transaction = Side Effects + Isolation Levels + Blocking Scopes + Blocking Regimes

### Side effects of parralelism

`Non-repeatable read` - Transaction read same data again and finds that data was changed.
`Fantom read` - Reads data, than same filter repeats and no data returned or more data returned.
`Lost update` - update same cell may be lost
`Dirty read` - read data from rolled back transaction

### Acid

Atomicity - all effects of transaction are done or none.

Consistency - while transaction runs, it cannot destroy consistency.

Isolation - transaction do not see changed doen by other transactions while running.

Durablity - if transaction finished succefully, thant data may never be lost.

### Read uncommitted

#### Suffers

- `Non repeatable read`
- `Fantom read`
- `Dirty read`

### Read committed

- `Non repeatable read`
- `Fantom read`

### Repeatable read

- `Fantom read`

### Serializable 

### Versioning

- Snapshot if MVCC
- On blocks

#### Blocking 

##### Scopes

- Row in table (document in collection)
- Key in index
- Raw data separation
- Table and related (collection)
- Database

##### Regime

- Shared allows select, but no delete or update
- Exclusive
- Update (read-select is shared, but update is exclusive, but shared read does not prevents exclusive) 

## Consistency models

Strong - after write, all processes see the data
Weak - does not guaranty that read after write will return value, need wait for consistency windows (e.g. read own writes may not work, or other reads see your writes may not work)
Eventual - eventually all will read update (form of Weak)
-- Casual - if process A said it wrote to B than B will read what A wrote, but no C
-- Read your writes
--- Session
-- Monotonic - if process have seen value, he will never see previous values
--- Read
--- Write

Possible impemetation on client sited to stick it to special Node.

### Balances

N(Nodes total)
R(Nodes from which can read)
W(Nodes which must be written to ensure write)

Depending on counts of these get different concurrency and availability balances.

### Cap

CAP(Consistency, Availability, Partition) balances.

### References

[1]: https://www.infoq.com/articles/cap-twelve-years-later-how-the-rules-have-changed/
[2]: https://habr.com/en/company/infopulse/blog/261097/
[3]: https://www.allthingsdistributed.com/2008/12/eventually_consistent.html