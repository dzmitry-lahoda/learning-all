select sum(dba_tables.blocks), dba_tables.TABLE_NAME 
from dba_tables 
group by dba_tables.TABLE_NAME 
having sum(dba_tables.blocks) > 0
order by sum(dba_tables.blocks) desc;

