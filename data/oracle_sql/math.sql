select (case when 0.5 < 1 then 1 else round(0.5,0) end) from dual; -- 1
SELECT ROUND(1.5) FROM DUAL; -- 2
SELECT ROUND(1.49999999999999999999999999999999999999) FROM DUAL; -- 1
SELECT ROUND(1.499999999999999999999999999999999999999) FROM DUAL; -- 2