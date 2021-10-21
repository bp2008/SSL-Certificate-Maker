# SSL-Certificate-Maker
A graphical tool for generating SSL certificates without any prior knowledge or command line tools.

![Screenshot](https://i.imgur.com/zBX2ueh.png)

## Usage

[Download from the releases tab](https://github.com/bp2008/SSL-Certificate-Maker/releases) and run the executable in a location where you have write permission, such as in a "Certificates" directory on your desktop.

For basic usage, you can simply click the `Make Certificate` button and find a new `localhost.pfx` file created next to the executable.  Nobody likes renewing self-signed certificates, so by default this program uses an expiration date that is 500 years after you started it.

## Trusting a Certificate

Self-signed certificates are not trusted by default, so you get security warnings whenever you try to connect to a web service that uses one.  You can work around this by instructing your operating system to trust the certificate.

The Windows OS allows you to easily trust new certificates just by double-clicking on the certificate file and going through the certificate installation process.  Specifically, I find that you need to choose the "Local Machine" store location and place your certificates in the "Trusted Root Certification Authorities" certificate store.  Try my [Certificate Trust Manager](https://github.com/bp2008/CertTrustManager) program to make this idiot-proof.

If you simply want a system to trust a certificate, you only need the signed public certificate (the `.cer` file if you are using the `.cer and .key` format). You can safely give the `.cer` file to anyone without compromising your private key.  `.pfx` files created by this program contain both the public and private keys, so you should keep them secure.

The `.cer and .key` format is common on Linux.  These are actually equivalent to `.pem` files, but I chose to use the extensions `.cer` and `.key` in order to differentiate between the public certificates and private keys.

If you want to use the certificate with IIS on Windows, you need to install both public and private keys, which in this case is easiest to do by installing the `.pfx` file to your Local Machine "Personal" certificate store.

*Be careful when trusting and sharing certificates.  If someone untrustworthy got ahold of the private key, they could use it to fool your computer into trusting any certificate they want!*

## Creating Your Own Certificate Authority

When you need to create many trusted certificates, it can be useful to sign them all with a common root certificate known as a **Certificate Authority** or "CA".  This way, you can have your operating system trust your CA, then any certificate your CA signs will automatically be trusted.

You can create a CA with this app by using the `CA` preset button before you click `Make Certificate`.  CA certificates are placed in the `CA` subfolder and become selectable in the `Certificate Authority` dropdown list.  Choose your CA from the Certificate Authority dropdown list to sign new certificates with your CA.

In this screenshot, I have instructed my computer to trust "My Very Trustworthy Certificate Authority".  Then I signed another certificate "MyESXiServer" with it, and now both are trusted.

![Trusted Certificate Chain](https://i.imgur.com/8tVWpbr.png)

If you want computers outside of your direct control to trust your certificates, you are in the wrong place.  You need to use a globally-trusted certificate authority like [LetsEncrypt - Free SSL/TLS Certificates](https://letsencrypt.org/) which is a more complicated process and requires you to register the domain names you wish to secure.

## Converting .cer and .key to .pfx, and vice-versa

This program also includes the ability to convert certificates and private keys between the `.cer and .key` and `.pfx` formats, via the Convert menu at the top left corner.

![Screenshot of Convert Certificates Panel](https://i.imgur.com/1jiXaJc.png)
