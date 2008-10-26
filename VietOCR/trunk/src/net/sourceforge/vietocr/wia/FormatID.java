/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package net.sourceforge.vietocr.wia;

/**
 *
 * @author Quan
 */
public enum FormatID {
    wiaFormatBMP("{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}"),
    wiaFormatPNG("{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}"),
    wiaFormatGIF("{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}"),
    wiaFormatJPEG("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}"),
    wiaFormatTIFF("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}");

    private final String value;

    FormatID(String value) {
        this.value = value;
    }

    public String getValue() {
        return value;
    }
}
