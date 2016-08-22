 with bytes_per_table as 
 (
   select
     (select sum(s.BLOCKS*8192)  from SYS.SYSFILES s) as BYTES_SIZE,
     (select sum(s.blocks*8192)  from all_tables s) as BYTES_SIZE2
   from dual   
  )
select 
  b.BYTES_SIZE/1024/1024/1024 as TOTAL_GBYTES_SIZE,
  b.BYTES_SIZE2/1024/1024/1024 as TOTAL_GBYTES_SIZE2
from bytes_per_table b