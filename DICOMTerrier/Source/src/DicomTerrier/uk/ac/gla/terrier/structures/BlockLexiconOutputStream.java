/*
 * Terrier - Terabyte Retriever 
 * Webpage: http://ir.dcs.gla.ac.uk/terrier 
 * Contact: terrier{a.}dcs.gla.ac.uk
 * University of Glasgow - Department of Computing Science
 * Information Retrieval Group
 * 
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 * the License for the specific language governing rights and limitations
 * under the License.
 *
 * The Original Code is BlockLexiconOutputStream.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures;
import java.io.File;
import java.io.IOException;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * An output stream for writing the lexicon to a file sequentially.
 * @author Douglas Johnson, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class BlockLexiconOutputStream extends LexiconOutputStream {
	/** A zero buffer for writing to the file.*/
	private static byte[] zeroBuffer =
		new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** 
	 * The number of different blocks in which a term appears.
	 * This is used only during the creation of the inverted
	 * file and it can be ignored afterwards.
	 */
	protected int blockFrequency;
	/**
	 * A default constructor.
	 */
	public BlockLexiconOutputStream() {
		super();
	}
	/**
	 * A constructor given the filename.
	 * @param filename java.lang.String the name of the lexicon file.
	 */
	public BlockLexiconOutputStream(String filename) {
		super(filename);
	}
	/**
	 * A constructor given the file.
	 * @param file java.io.File the lexicon file.
	 */
	public BlockLexiconOutputStream(File file) {
		super(file);
	}
	/**
	 * Write a lexicon entry.
	 * @return the number of bytes written if there is no error, otherwise returns -1 in case of EOF
	 * @throws IOException if an I/O error occurs
	 * @param term the string representation of the term
	 * @param termId the terms integer identifier
	 * @param documentFrequency the term's document frequency in the collection
	 * @param termFrequency the term's frequency in the collection
	 * @param endOffset the term's ending byte offset in the inverted file
	 * @param endBitOffset the term's ending byte bit-offset in the inverted file
	 */
	public int writeNextEntry(
		String term,
		int termId,
		int documentFrequency,
		int termFrequency,
		int blockFrequency,
		long endOffset,
		byte endBitOffset)
		throws IOException {
		byte[] tmpBytes = term.getBytes();
		int length = tmpBytes.length;
		lexiconStream.write(tmpBytes, 0, length);
		lexiconStream.write(
			zeroBuffer,
			0,
			ApplicationSetup.STRING_BYTE_LENGTH - length);
		lexiconStream.writeInt(termId);
		lexiconStream.writeInt(documentFrequency);
		lexiconStream.writeInt(blockFrequency);
		lexiconStream.writeInt(termFrequency);
		lexiconStream.writeLong(endOffset);
		lexiconStream.writeByte(endBitOffset);
		return Lexicon.lexiconEntryLength;
	}
	/**
	 * Write a lexicon entry.
	 * @return the number of bytes written if there is no error, otherwise returns -1 in case of EOF
	 * @throws java.io.IOException if an I/O error occurs
	 * @param term the byte array representation of the term
	 * @param termId the terms integer identifier
	 * @param documentFrequency the term's document frequency in the collection
	 * @param termFrequency the term's frequency in the collection
	 * @param endOffset the term's ending byte offset in the inverted file
	 * @param endBitOffset the term's ending byte bit-offset in the inverted file
	 */
	public int writeNextEntry(
		byte[] term,
		int termId,
		int documentFrequency,
		int termFrequency,
		int blockFrequency,
		long endOffset,
		byte endBitOffset)
		throws IOException {
		lexiconStream.write(term, 0, term.length);
		lexiconStream.writeInt(termId);
		lexiconStream.writeInt(documentFrequency);
		lexiconStream.writeInt(blockFrequency);
		lexiconStream.writeInt(termFrequency);
		lexiconStream.writeLong(endOffset);
		lexiconStream.writeByte(endBitOffset);
		return Lexicon.lexiconEntryLength;
	}
	/**
	 * Sets the block frequency for the given term
	 * @param blockFrequency The new block frequency
	 */
	public void setBF(int blockFrequency) {
		this.blockFrequency = blockFrequency;
	}
}
