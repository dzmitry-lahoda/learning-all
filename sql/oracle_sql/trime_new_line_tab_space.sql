    select 
      '    example with new line ' || CHR(10) || CHR(13) || CHR(09) || ' ' as original,
      translate('    example with new line ' || CHR(10) || CHR(13) || CHR(09) || ' ', chr(10) || chr(13) || chr(09), ' ')  as replaced,
      trim(translate('    example with new line ' || CHR(10) || CHR(13) || CHR(09) || ' ', chr(10) || chr(13) || chr(09), ' '))  as trimmed
    from dual;