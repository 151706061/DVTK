// $ANTLR : "terrier.g" -> "TerrierQueryParser.java"$

package uk.ac.gla.terrier.querying.parser;
import antlr.TokenStreamSelector;

public interface TerrierQueryParserTokenTypes {
	int EOF = 1;
	int NULL_TREE_LOOKAHEAD = 3;
	int NUM_FLOAT = 4;
	int PERIOD = 5;
	int DIT = 6;
	int INT = 7;
	int NUM_INT = 8;
	int ALPHANUMERIC_CHAR = 9;
	int ALPHANUMERIC = 10;
	int COLON = 11;
	int GT = 12;
	int EQ = 13;
	int LT = 14;
	int HAT = 15;
	int REQUIRED = 16;
	int NOT_REQUIRED = 17;
	int OPEN_PAREN = 18;
	int CLOSE_PAREN = 19;
	int PROXIMITY = 20;
	int IGNORED = 21;
}
