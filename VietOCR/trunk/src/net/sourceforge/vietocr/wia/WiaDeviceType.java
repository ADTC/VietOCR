/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.sourceforge.vietocr.wia;

/**
 *
 * @author Quan
 */
public enum WiaDeviceType {
    UnspecifiedDeviceType(0),
    ScannerDeviceType(1),
    CameraDeviceType(2),
    VideoDeviceType(3);
    
    private final int value;

    WiaDeviceType(int value) {
        this.value = value;
    }
    
    public int getValue() {
        return value;
    }
}
