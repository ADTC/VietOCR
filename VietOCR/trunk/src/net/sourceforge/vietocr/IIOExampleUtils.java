/*
 * $RCSfile: IIOExampleUtils.java,v $
 *
 * 
 * Copyright (c) 2005 Sun Microsystems, Inc. All  Rights Reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met: 
 * 
 * - Redistribution of source code must retain the above copyright 
 *   notice, this  list of conditions and the following disclaimer.
 * 
 * - Redistribution in binary form must reproduce the above copyright
 *   notice, this list of conditions and the following disclaimer in 
 *   the documentation and/or other materials provided with the
 *   distribution.
 * 
 * Neither the name of Sun Microsystems, Inc. or the names of 
 * contributors may be used to endorse or promote products derived 
 * from this software without specific prior written permission.
 * 
 * This software is provided "AS IS," without a warranty of any 
 * kind. ALL EXPRESS OR IMPLIED CONDITIONS, REPRESENTATIONS AND 
 * WARRANTIES, INCLUDING ANY IMPLIED WARRANTY OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE OR NON-INFRINGEMENT, ARE HEREBY
 * EXCLUDED. SUN MIDROSYSTEMS, INC. ("SUN") AND ITS LICENSORS SHALL 
 * NOT BE LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF 
 * USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS
 * DERIVATIVES. IN NO EVENT WILL SUN OR ITS LICENSORS BE LIABLE FOR 
 * ANY LOST REVENUE, PROFIT OR DATA, OR FOR DIRECT, INDIRECT, SPECIAL,
 * CONSEQUENTIAL, INCIDENTAL OR PUNITIVE DAMAGES, HOWEVER CAUSED AND
 * REGARDLESS OF THE THEORY OF LIABILITY, ARISING OUT OF THE USE OF OR
 * INABILITY TO USE THIS SOFTWARE, EVEN IF SUN HAS BEEN ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGES. 
 * 
 * You acknowledge that this software is not designed or intended for 
 * use in the design, construction, operation or maintenance of any 
 * nuclear facility. 
 *
 * $Revision: 1.1 $
 * $Date: 2005/02/11 05:20:14 $
 * $State: Exp $
 */
package net.sourceforge.vietocr;

import javax.imageio.metadata.IIOMetadata;
import javax.imageio.metadata.IIOMetadataNode;
import org.w3c.dom.NamedNodeMap;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

/**
 * This class contains static utility methods for example code.
 */
public class IIOExampleUtils {
    private static final int INDENT = 2;

    private static void indent(int level, int extra) {
        int indent = level*INDENT + extra;
        for(int i = 0; i < indent; i++) {
            System.out.print(" ");
        }
    }

    public static void printMetadata(IIOMetadata metadata,
                                     String metadataFormat) {
        Node tree = metadata.getAsTree(metadataFormat);
        printNode(tree, 0);
    }

    public static void printNode(Node node, int level) {
        indent(level, 0);

        IIOMetadataNode iioNode = (IIOMetadataNode)node;
        System.out.print("<"+iioNode.getNodeName());

        NodeList children = iioNode.getChildNodes();
        int numChildren = children.getLength();

        NamedNodeMap attributes = iioNode.getAttributes();
        int numAttributes = attributes.getLength();
        for(int i = 0; i < numAttributes; i++) {
            Node attribute = attributes.item(i);
            if(i > 0) {
                System.out.println("");
                indent(level, iioNode.getNodeName().length()+1);
            }
            System.out.print(" "+attribute.getNodeName()+"=");
            System.out.print("\""+attribute.getNodeValue()+"\"");
        }

        String nodeValue = iioNode.getNodeValue();

        if(numChildren == 0 && nodeValue == null) {
            System.out.println("/>");
        } else {
            System.out.println(">");
        }

        if(nodeValue != null) {
            indent(level, INDENT);
            System.out.println(nodeValue);
        }

        for(int i = 0; i < numChildren; i++) {
            printNode(children.item(i), level + 1);
        }

        if(numChildren > 0 || nodeValue != null) {
            indent(level, 0);
            System.out.println("</"+iioNode.getNodeName()+">");
        }
    }
}
