header {
package uk.ac.gla.terrier.querying.parser;
}


class TerrierFloatLexer extends Lexer;
options {
	exportVocab=Numbers;
}

tokens{
	NUM_FLOAT;
}

protected
PERIOD:	'.'	;

protected
DIT:	'0'..'9';

protected
INT:	(DIT)+	;

//a query term
NUM_INT: 	INT ('.' INT { _ttype = NUM_FLOAT; } )?;

