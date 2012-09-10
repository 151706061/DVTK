// $ANTLR : "terrier.g" -> "TerrierQueryParser.java"$

package uk.ac.gla.terrier.querying.parser;
import antlr.TokenStreamSelector;

import antlr.TokenBuffer;
import antlr.TokenStreamException;
import antlr.TokenStreamIOException;
import antlr.ANTLRException;
import antlr.LLkParser;
import antlr.Token;
import antlr.TokenStream;
import antlr.RecognitionException;
import antlr.NoViableAltException;
import antlr.MismatchedTokenException;
import antlr.SemanticException;
import antlr.ParserSharedInputState;
import antlr.collections.impl.BitSet;

public class TerrierQueryParser extends antlr.LLkParser       implements TerrierQueryParserTokenTypes
 {

	private TokenStreamSelector selector;
	public void setSelector(TokenStreamSelector s)
	{
		selector = s;
	}

protected TerrierQueryParser(TokenBuffer tokenBuf, int k) {
  super(tokenBuf,k);
  tokenNames = _tokenNames;
}

public TerrierQueryParser(TokenBuffer tokenBuf) {
  this(tokenBuf,2);
}

protected TerrierQueryParser(TokenStream lexer, int k) {
  super(lexer,k);
  tokenNames = _tokenNames;
}

public TerrierQueryParser(TokenStream lexer) {
  this(lexer,2);
}

public TerrierQueryParser(ParserSharedInputState state) {
  super(state,2);
  tokenNames = _tokenNames;
}

	public final Query  query() throws RecognitionException, TokenStreamException {
		Query q;
		
		q=null;
		
		try {      // for error handling
			if ((_tokenSet_0.member(LA(1))) && (_tokenSet_1.member(LA(2)))) {
				q=impliedMultiTermQuery();
			}
			else if ((LA(1)==OPEN_PAREN) && (LA(2)==ALPHANUMERIC||LA(2)==REQUIRED||LA(2)==NOT_REQUIRED)) {
				q=explicitMultiTermQuery();
			}
			else if ((LA(1)==ALPHANUMERIC) && (LA(2)==COLON)) {
				q=fieldQuery();
			}
			else if ((LA(1)==ALPHANUMERIC) && (LA(2)==GT)) {
				q=gtQuery();
			}
			else if ((LA(1)==ALPHANUMERIC) && (LA(2)==EQ)) {
				q=eqQuery();
			}
			else if ((LA(1)==ALPHANUMERIC) && (LA(2)==LT)) {
				q=ltQuery();
			}
			else if ((LA(1)==REQUIRED||LA(1)==NOT_REQUIRED) && (LA(2)==ALPHANUMERIC||LA(2)==OPEN_PAREN)) {
				q=requirementQuery();
			}
			else if ((LA(1)==ALPHANUMERIC) && (LA(2)==EOF||LA(2)==HAT)) {
				q=singleTermQuery();
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_2);
		}
		return q;
	}
	
	public final Query  impliedMultiTermQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		q= null; MultiTermQuery mtq = new MultiTermQuery(); Query child = null;
		
		try {      // for error handling
			{
			int _cnt47=0;
			_loop47:
			do {
				if ((_tokenSet_0.member(LA(1)))) {
					{
					switch ( LA(1)) {
					case OPEN_PAREN:
					{
						child=explicitMultiTermQuery();
						break;
					}
					case REQUIRED:
					case NOT_REQUIRED:
					{
						child=requirementQuery();
						break;
					}
					default:
						if ((LA(1)==ALPHANUMERIC) && (_tokenSet_3.member(LA(2)))) {
							child=singleTermQuery();
						}
						else if ((LA(1)==ALPHANUMERIC) && (LA(2)==COLON)) {
							child=fieldQuery();
						}
						else if ((LA(1)==ALPHANUMERIC) && (LA(2)==GT)) {
							child=gtQuery();
						}
						else if ((LA(1)==ALPHANUMERIC) && (LA(2)==EQ)) {
							child=eqQuery();
						}
						else if ((LA(1)==ALPHANUMERIC) && (LA(2)==LT)) {
							child=ltQuery();
						}
					else {
						throw new NoViableAltException(LT(1), getFilename());
					}
					}
					}
					mtq.add(child);
				}
				else {
					if ( _cnt47>=1 ) { break _loop47; } else {throw new NoViableAltException(LT(1), getFilename());}
				}
				
				_cnt47++;
			} while (true);
			}
			q= mtq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_2);
		}
		return q;
	}
	
	public final Query  explicitMultiTermQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		Token  closeP = null;
		q= null; MultiTermQuery mtq = new MultiTermQuery(); Query child = null;
		
		try {      // for error handling
			match(OPEN_PAREN);
			{
			int _cnt51=0;
			_loop51:
			do {
				if ((LA(1)==ALPHANUMERIC||LA(1)==REQUIRED||LA(1)==NOT_REQUIRED) && (_tokenSet_4.member(LA(2)))) {
					{
					if ((LA(1)==ALPHANUMERIC) && (_tokenSet_5.member(LA(2)))) {
						child=singleTermQuery();
					}
					else if ((LA(1)==ALPHANUMERIC) && (LA(2)==COLON)) {
						child=fieldQuery();
					}
					else if ((LA(1)==REQUIRED||LA(1)==NOT_REQUIRED)) {
						child=requirementQuery();
					}
					else {
						throw new NoViableAltException(LT(1), getFilename());
					}
					
					}
					mtq.add(child);
				}
				else {
					if ( _cnt51>=1 ) { break _loop51; } else {throw new NoViableAltException(LT(1), getFilename());}
				}
				
				_cnt51++;
			} while (true);
			}
			{
			if ((LA(1)==CLOSE_PAREN) && (_tokenSet_6.member(LA(2)))) {
				closeP = LT(1);
				match(CLOSE_PAREN);
			}
			else if ((_tokenSet_6.member(LA(1))) && (_tokenSet_7.member(LA(2)))) {
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
			}
			if (closeP == null){ /* WARN missing '(' */ }
			q= mtq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_6);
		}
		return q;
	}
	
	public final Query  fieldQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		Token  f = null;
		q= null; FieldQuery fq = new FieldQuery(); Query child = null;
		
		try {      // for error handling
			f = LT(1);
			match(ALPHANUMERIC);
			fq.setField(f.getText());
			match(COLON);
			{
			switch ( LA(1)) {
			case ALPHANUMERIC:
			{
				child=singleTermQuery();
				break;
			}
			case OPEN_PAREN:
			{
				child=explicitMultiTermQuery();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			fq.setChild(child);
			q = fq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_6);
		}
		return q;
	}
	
	public final Query  gtQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		Token  f = null;
		Token  v = null;
		Token  p_1 = null;
		q= null; GTFieldQuery fq = new GTFieldQuery(); Query child = null;
		
		try {      // for error handling
			f = LT(1);
			match(ALPHANUMERIC);
			fq.setField(f.getText());
			match(GT);
			{
			v = LT(1);
			match(ALPHANUMERIC);
			}
			
				fq.setStartTerm(v.getText());
			{
			switch ( LA(1)) {
			case GT:
			{
				match(GT);
				p_1 = LT(1);
				match(ALPHANUMERIC);
				
					fq.setField(v.getText());
					fq.setStartTerm(p_1.getText());
					fq.setEndTerm(f.getText());
				break;
			}
			case EOF:
			case ALPHANUMERIC:
			case REQUIRED:
			case NOT_REQUIRED:
			case OPEN_PAREN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			q = fq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_8);
		}
		return q;
	}
	
	public final Query  eqQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		Token  f = null;
		q= null; EQFieldQuery fq = new EQFieldQuery(); Query child = null;
		
		try {      // for error handling
			f = LT(1);
			match(ALPHANUMERIC);
			fq.setField(f.getText());
			match(EQ);
			{
			child=singleTermQuery();
			}
			fq.setChild(child);
			q = fq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_8);
		}
		return q;
	}
	
	public final Query  ltQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		Token  f = null;
		Token  v = null;
		Token  p_1 = null;
		q= null; LTFieldQuery fq = new LTFieldQuery(); Query child = null;
		
		try {      // for error handling
			f = LT(1);
			match(ALPHANUMERIC);
			fq.setField(f.getText());
			match(LT);
			{
			v = LT(1);
			match(ALPHANUMERIC);
			}
			fq.setEndTerm(v.getText());
			{
			switch ( LA(1)) {
			case LT:
			{
				match(LT);
				p_1 = LT(1);
				match(ALPHANUMERIC);
				
					fq.setField(v.getText());
					fq.setStartTerm(f.getText());
					fq.setEndTerm(p_1.getText());
				break;
			}
			case EOF:
			case ALPHANUMERIC:
			case REQUIRED:
			case NOT_REQUIRED:
			case OPEN_PAREN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			q = fq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_8);
		}
		return q;
	}
	
	public final Query  requirementQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		q= null; RequirementQuery rq = new RequirementQuery(); Query child = null;
		
		try {      // for error handling
			{
			switch ( LA(1)) {
			case REQUIRED:
			{
				match(REQUIRED);
				break;
			}
			case NOT_REQUIRED:
			{
				match(NOT_REQUIRED);
				rq.setRequired(false);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			{
			if ((LA(1)==ALPHANUMERIC) && (_tokenSet_5.member(LA(2)))) {
				child=singleTermQuery();
			}
			else if ((LA(1)==ALPHANUMERIC) && (LA(2)==COLON)) {
				child=fieldQuery();
			}
			else if ((LA(1)==OPEN_PAREN)) {
				child=explicitMultiTermQuery();
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
			}
			rq.setChild(child);
			q = rq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_6);
		}
		return q;
	}
	
	public final Query  singleTermQuery() throws RecognitionException, TokenStreamException {
		Query q;
		
		Token  qt = null;
		Token  w_f = null;
		Token  w_i = null;
		q= null; SingleTermQuery stq = new SingleTermQuery();
		
		try {      // for error handling
			qt = LT(1);
			match(ALPHANUMERIC);
			stq.setTerm(qt.getText());
			{
			switch ( LA(1)) {
			case HAT:
			{
				match(HAT);
				selector.push("numbers");
				{
				switch ( LA(1)) {
				case NUM_FLOAT:
				{
					w_f = LT(1);
					match(NUM_FLOAT);
					stq.setWeight(Double.parseDouble(w_f.getText())); selector.pop();
					break;
				}
				case NUM_INT:
				{
					w_i = LT(1);
					match(NUM_INT);
					stq.setWeight(Double.parseDouble(w_i.getText())); selector.pop();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				break;
			}
			case EOF:
			case ALPHANUMERIC:
			case REQUIRED:
			case NOT_REQUIRED:
			case OPEN_PAREN:
			case CLOSE_PAREN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			q = stq;
		}
		catch (RecognitionException ex) {
			reportError(ex);
			recover(ex,_tokenSet_6);
		}
		return q;
	}
	
	
	public static final String[] _tokenNames = {
		"<0>",
		"EOF",
		"<2>",
		"NULL_TREE_LOOKAHEAD",
		"NUM_FLOAT",
		"PERIOD",
		"DIT",
		"INT",
		"NUM_INT",
		"ALPHANUMERIC_CHAR",
		"ALPHANUMERIC",
		"COLON",
		"GT",
		"EQ",
		"LT",
		"HAT",
		"REQUIRED",
		"NOT_REQUIRED",
		"OPEN_PAREN",
		"CLOSE_PAREN",
		"PROXIMITY",
		"IGNORED"
	};
	
	private static final long[] mk_tokenSet_0() {
		long[] data = { 459776L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_0 = new BitSet(mk_tokenSet_0());
	private static final long[] mk_tokenSet_1() {
		long[] data = { 523266L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_1 = new BitSet(mk_tokenSet_1());
	private static final long[] mk_tokenSet_2() {
		long[] data = { 2L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_2 = new BitSet(mk_tokenSet_2());
	private static final long[] mk_tokenSet_3() {
		long[] data = { 492546L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_3 = new BitSet(mk_tokenSet_3());
	private static final long[] mk_tokenSet_4() {
		long[] data = { 1018882L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_4 = new BitSet(mk_tokenSet_4());
	private static final long[] mk_tokenSet_5() {
		long[] data = { 1016834L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_5 = new BitSet(mk_tokenSet_5());
	private static final long[] mk_tokenSet_6() {
		long[] data = { 984066L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_6 = new BitSet(mk_tokenSet_6());
	private static final long[] mk_tokenSet_7() {
		long[] data = { 1047554L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_7 = new BitSet(mk_tokenSet_7());
	private static final long[] mk_tokenSet_8() {
		long[] data = { 459778L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_8 = new BitSet(mk_tokenSet_8());
	
	}
