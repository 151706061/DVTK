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
 * The Original Code is FilePosition.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures;
/**
 * Models a position within a file as the offset in bytes
 * and the offset in bits in that byte. For example, an instance
 * of the class FilePosition that points to the 10th bit of 
 * a file should be constructed with a byte offset of 1 and 
 * a bit offset of 2.
 * @author Craig Macdonald &amp; Vassilis Plachouras
 */
public class FilePosition
{
	/** The offset within a file in bytes. */
	public long Bytes;
	
	/** The offset in bits within the pointed byte. */
	public byte Bits;
	
	/** 
	 * Creates an instance of the class from the given 
	 * byte and bit offsets.
	 * @param bytesPosition long the given byte offset.
	 * @param bitsPosition byte the given bit offset.
	 */
	public FilePosition(long bytesPosition, byte bitsPosition) {
		Bytes = bytesPosition; Bits = bitsPosition;
	}
}
