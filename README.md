# DigitalMint
A proof-of-concept mint for centralised digital currencies  
  
While cryptocurrencies often using mining approaches to create value, Central Banks must issue Fiat currency and be able to create/remove money upon demand. This program creates digital coins which are digitially signed (Sha512WithECDSA).  
  
Implemented in this version is the ability to transfer coins and have multiple holders, any one of which is authorised to spend (think joint bank account).  In practice, transfers require the Mint (or other competent body) to act as middle-man and update the public key of the current holder(s) and sign the packet.  
  
The program highlights the issue of requiring end users to maintain a keypair, so that they can sign transfer requests or update their keypair. A solution may be in the form of a government portal which holds the keypair and automatically refreshes expiring keys.  
  
The code currently uses ECDSA to sign the coins, but RSA code is included too.  

The code has been recently updated to include an API and supports an SQL server backend using Entity Framework.
