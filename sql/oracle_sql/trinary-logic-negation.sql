create table attributes
(
is_deletable VARCHAR2(1 CHAR),
resource_stat VARCHAR2(3 CHAR)
)
insert into attributes values (NULL, 'ACT');
insert into attributes values ('Y', 'ACT');
insert into attributes values ('N', 'ACT');

select * from attributes; -- 3 row

update attributes set resource_stat = 'DEL' where is_deletable = 'Y'; -- 1 row, bad
update attributes set resource_stat = 'DEL';
update attributes set resource_stat = 'DEL' where is_deletable <> 'N'; -- 1 row, bad
update attributes set resource_stat = 'DEL' where is_deletable != 'N'; -- 1 row, bad

update attributes set resource_stat = 'DEL' where is_deletable = 'N' or is_deletable is null; -2 row, good



