
-- http://stackoverflow.com/questions/18701984/displaying-the-hex-value-of-a-string-from-a-oracle-varchar2
-- http://stackoverflow.com/questions/5871369/oracle-how-do-i-get-the-actual-size-of-a-specific-row
-- https://github.com/datajunkie007/fusql/blob/master/Oracle/oracle_table_size.sql
-- http://stackoverflow.com/questions/18701984/displaying-the-hex-value-of-a-string-from-a-oracle-varchar2
-- https://docs.oracle.com/database/121/CNCPT/logical.htm
-- http://stackoverflow.com/questions/264914/how-do-i-calculate-tables-size-in-oracle

SELECT avg_row_len, t.blocks, t.*   
FROM  all_tables t;

-- UTL_RAW.LENGTH(DUMMY) 
select ROWID,vsize(d.DUMMY) from dual d;

-- vsize and LENGTH does not work on *. 
-- vsize and LENGTH is NULL on NULL
with n as
  (
    select NULL as DUMMY, dump('123') as D , RAWTOHEX('ASD') as R, HEXTORAW(44) as H, UTL_RAW.CAST_TO_VARCHAR2('415344') as U from dual
  )
select vsize(n.DUMMY), LENGTH(n.dummy),LENGTH(n.R), UTL_RAW.LENGTH(n.R), n.* from n;



SELECT 
  coalesce(vsize(sf.FNAME),0),  
  LENGTH(sf.FNAME), 
  sf.*   
  FROM SYS.SYSFILES sf;

select * from V$PARAMETER where NAME like '%size%';

select * from SYS.SYSFILES; 
select * from SYS.SYSTEM_PRIVILEGE_MAP;
select * from SYS.SYSCATALOG;
select * from SYS.SYSSEGOBJ;
select * from dba_tables;
select sum(dba_tables.blocks),dba_tables.TABLE_NAME from dba_tables group by dba_tables.TABLE_NAME order by sum(dba_tables.blocks) desc;
select * from sys.dba_Data_files order by blocks desc;

select s.blocks as SYSFILES_BLOCKS, f.blocks as dba_Data_files_BLOCKS, s.*, f.* from SYS.SYSFILES s join sys.dba_Data_files f on s.fname = f.FILE_NAME  order by f.blocks desc;

select * from dba_segments;

select * from V$SYSTEM_PARAMETER;
SHOW PARAMETERS;
SHOW SPPARAMETERS;

-- was not able to run this well
select * from table_size;
select * from SYSMAN;
select * from SYSTEM;
select * from SYSAUX;
select * from SYS;


