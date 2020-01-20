create table field_values
(
is_user_deletable VARCHAR2(1 CHAR),
resource_mark VARCHAR2(3 CHAR)
)

insert into field_values values (NULL, NULL);
insert into field_values values ('Y', NULL);
insert into field_values values ('N', NULL);

select * from field_values; -- 3 row

-- 1 row, bad
update field_values set resource_mark = 'D' where is_user_deletable = 'Y'; 
update field_values set resource_mark = 'D' where is_user_deletable <> 'N'; 
update field_values set resource_mark = 'D' where is_user_deletable != 'N'; 
update field_values set resource_mark = 'D' where is_user_deletable not in ('Y');

-- 2 rows, good, but needs more SQL and more comparisons. Does DB optimizes?
update field_values set resource_mark = 'D' where is_user_deletable = 'N' or is_user_deletable is null; 

-- it better to have ('Y' and NULL) or ('N' and NULL), but not 'Y' and 'N' and NULL (avoid quires with https://en.wikipedia.org/wiki/Three-valued_logic)
-- adding new column which is 'Y' or 'N' only into existing table may be very long locking operation on existing table (or running long DML script)


