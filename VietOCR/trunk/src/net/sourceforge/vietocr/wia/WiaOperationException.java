/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package net.sourceforge.vietocr.wia;

import com.jacob.com.ComException;

/**
 *
 * @author Quan
 */
public class WiaOperationException extends Exception {

    private WiaScannerError _errorCode;

    public WiaOperationException(WiaScannerError errorCode) {
        super();
        _errorCode = errorCode;
    }

    public WiaOperationException(String message, WiaScannerError errorCode) {
        super(message);
        _errorCode = errorCode;
    }

    public WiaOperationException(String message, Exception innerException) {
        super(message, innerException);
//        COMException comException = (COMException) innerException;
//
//        if (comException != null) {
//            _errorCode = (WiaScannerError) comException.getErrorCode();
//        }
    }

    public WiaOperationException(String message, Exception innerException, WiaScannerError errorCode) {
        super(message, innerException);
        _errorCode = errorCode;
    }

//    public WiaOperationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
//        super(info, context);
//        info.AddValue("ErrorCode", (long) _errorCode);
//    }

    public WiaScannerError getErrorCode() {
        return _errorCode;
    }

    void setErrorCode(WiaScannerError value) {
        _errorCode = value;
    }

//    void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
//        super().GetObjectData(info, context);
//        _errorCode = (WiaScannerError) info.GetUInt32("ErrorCode");
//    }
}
