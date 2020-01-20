declare
  cursor c is SELECT * from dual;
  n number;
begin
  n := 42;
  dbms_output.put_line(n);
end;