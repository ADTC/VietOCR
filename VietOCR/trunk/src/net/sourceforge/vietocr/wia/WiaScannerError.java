/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.sourceforge.vietocr.wia;

/**
 *
 * @author Quan
 */
public enum WiaScannerError {

    LibraryNotInstalled(0x80040154), OutputFileExists(0x80070050), ScannerNotAvailable(0x80210015), OperationCancelled(0x80210064);

    private final long value;

    WiaScannerError(long value) {
        this.value = value;
    }

    long getValue() {
        return value;
    }
}