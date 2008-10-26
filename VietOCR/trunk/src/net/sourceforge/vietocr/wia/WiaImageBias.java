/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package net.sourceforge.vietocr.wia;

/**
 *
 * @author Quan
 */
public enum WiaImageBias {
    MinimizeSize (65536), MaximizeQuality(131072);
    
    private final int value;

    WiaImageBias(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
}
