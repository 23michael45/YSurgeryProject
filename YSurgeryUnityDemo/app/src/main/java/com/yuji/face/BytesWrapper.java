package com.yuji.face;

public class BytesWrapper {
    public byte[] bytes;
    BytesWrapper(byte[] b)
    {
        setBytes(b);
    }

    public void setBytes(byte[] b) {
        this.bytes = b;
    }

    public byte[] getBytes() {
        return this.bytes;
    }
}
