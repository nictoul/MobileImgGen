# MobileImgGen

This console app generate all image size for android. You specify the original one and the size ratio. MobileImgConverter will create all images.
MobileImgGen use ImageMagick to make all image resizing.

Android image size:

    LDPI:    36x36   0.75
    MDPI:    48x48   1.0
    HDPI:    72x72   1.5
    XHDPI:   96x96   2.0
    XXHDPI:  144x144 3.0
    XXXHDPI: 192x192 4.0

example: 
    
    MobileImgGen -s 3.0 -i "C:\temp\MobileImgGen\icon.png"

This command specify that input file is an XXHDPI image (XXHDPI is 3.0x bigger than LDPI)  
All other size will be created

## requirements

before using MobileImgGen you must install imageMagick