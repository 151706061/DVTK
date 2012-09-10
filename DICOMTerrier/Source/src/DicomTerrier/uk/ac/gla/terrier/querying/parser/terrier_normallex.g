header {
package uk.ac.gla.terrier.querying.parser;
}


// ----------------------------------------------------------------------------
// the main lexer

class TerrierLexer extends Lexer;

options 
{
    k = 1;
	/* we need to set this for  antlr < 2.7.5, so the set complement
	 * functions correctly. */
	charVocabulary = '\3'..'\377';
	exportVocab=Main;
	importVocab=Numbers;
	defaultErrorHandler=false;
}

protected
ALPHANUMERIC_CHAR:	'0'..'9'|'a'..'z'|'A'..'Z'|'\200'..'\377'|'_'|'.'|'-';

ALPHANUMERIC:   (ALPHANUMERIC_CHAR)+;

//used for fields and boosting weights
COLON:        ':'
     ;

//used for boolean 'greater-then' query
GT:        '>'
     ;

//used for boolean 'equal-to' query
EQ:        '='
     ;

//used for boolean 'less-then' query
LT:        '<'
     ;

//before weights
HAT:          '^'
   ;

//start and end of a phrase
//QUOTE:        '\"'
//    ;

//required token
REQUIRED:     '+'
        ;

//not required token
NOT_REQUIRED: '!';

//opening parenthesis
OPEN_PAREN: '(';

//closing parenthesis
CLOSE_PAREN: ')';

//proximity operator
PROXIMITY: '~';


IGNORED:
    (~(
		':'|'^'|'-'|'+'|'('|')'|'~'|'!'|
		'A'..'Z'|'a'..'z'|'0'..'9'|'_'|'.'|'\200'..'\377'))
   { $setType(Token.SKIP); }
;
