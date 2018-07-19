// boost-regex.cpp : Defines the entry point for the console application.
//

//https://www.boost.org/doc/libs/1_66_0/libs/regex/doc/html/boost_regex/syntax/perl_syntax.html
#include "stdafx.h"
#include "../packages/boost.1.66.0.0/lib/native/include/boost/regex.hpp"
#include <iostream>
#include <string>

int main()
{
	boost::regex e("0\.1");
	std::string variable("0.1;0.01;0.001");
	auto match1 = regex_match(variable, e);
	std::cout << match1 << std::endl;

	//
	//boost::regex b("(.*0\.01($|;.*))");
	//boost::regex b("((;?|.*?;+?|[^\n])0\.01($|;.*))");
	//boost::regex b("([^\n]+0\.1($|;.*))");
	boost::regex b("((^|.*;)0\.1($|;.*))");
	std::string variable10("10.1;10.01;10.001");
	auto match10 = regex_match(variable10, b);
	std::cout << match10 << std::endl;

	//
	boost::regex r("((^|.*;)AAAA($|;.*))");
	//boost::regex r("0\.01;");
	std::string variabler("AAAA;BB");
	auto matchr = regex_match(variabler, r);
	std::cout << matchr << std::endl;

    return 0;
}

