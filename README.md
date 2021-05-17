# DigitalMint
A proof-of-concept mint for Central Bank Digital Currency (CBDC)  
  
While cryptocurrencies often using mining approaches to create value, Central Banks must issue Fiat currency and be able to create/remove money upon demand. Typically referred to as CBDC (Central Bank Digital Currency), this program creates digital coins which are digitially signed (Sha512WithECDSA).  
  
Implemented in this version is the ability to create, transfer and search for coins. Each coin can have multiple holders, any one of which is authorised to spend (think joint bank account).  In practice, transfers require the Mint (or other competent body) to act as middle-man and update the public key of the current holder(s) and sign the packet.  
  
The program highlights the issue of requiring end users to maintain a keypair, so that they can sign transfer requests or update their keypair. A solution may be in the form of a government portal, or even bank, which holds the keypair and automatically refreshes expiring keys.

Coins, in Digital Mint, come with the concept of 'Purpose' which can be used to limit spendability.  For example, it is possible to mark coins as 'social benefits', which may prevent the holder from purchasing alcohol, narcotics, etc.  Another use case may be the purchase of defense related equipment.  
  
The code currently uses ECDSA to sign the coins, but RSA code is included too and can be used with minor edits.  
  
  
How to build  
  
1. Create a SQL database name LibMint  
2. Create a SQL login called LibMint and give it password.  
3. Locate MintDBContext and add your details to the SQL connection string  
4. Set MintAPI as the startup project  
5. In Package manager Add-Migration and Update-Database  
6. Right-click the solution and select 'Set startup projects...'  
7. Select 'Multiple startup projects'  
8. Start MintAPI and MintAPITest 
9. In MintAPITest make sure the requests are pointed to the correct URL and port   
10. Run  
