using System;

void fail(bool v, string msg) => pass(!v, msg); fail(false, "Test");
void pass(bool v, string msg)  {if (!v) throw new Exception(msg);} pass(true, "Test");

string GetNullString() => null;
string GetString() => string.Empty;

object GetStringObject() => string.Empty;

object GetObject() => new object();

object GetNullObject() => null;


object GetNullAsStringObject()
{
    string s = null;
    return s; 
}

pass(null == null, "");
pass(Object.ReferenceEquals(null,null),"");
fail(GetNullAsStringObject() is object, "");

fail(GetNullString() is string, "Seems null is casted to Some<string>(None) and then matched into non");
fail(GetNullAsStringObject() is string, "Not as string event if return variable is string?");
string ss = null; fail(ss is string, "Variable of string is not string");
object obj = null; fail(obj is object, "Variable of string is not string");
pass(GetNullAsStringObject() is null, "");
pass(GetNullObject() == (object)GetNullString(), "");
pass(GetNullAsStringObject() == null, "");
pass(GetNullString() is null, "");
pass(GetNullString() == null, "");
pass((object)GetNullString() == GetNullAsStringObject(), "");

pass(GetString() is string, "String is string");
pass(GetString() is string, "String is string");
pass(GetString() is string, "String is string");