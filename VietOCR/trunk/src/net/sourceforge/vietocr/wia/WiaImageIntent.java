/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package net.sourceforge.vietocr.wia;

/**
 *
 * @author Quan
 */
public enum WiaImageIntent {
    UnspecifiedIntent(0),
    ColorIntent(1),
    GrayscaleIntent(2),
    TextIntent(4);

    private final int value;

    WiaImageIntent(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
}
