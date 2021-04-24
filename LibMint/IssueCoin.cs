using LibMintModels;
using LibMockBackend;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibMint
{
    public class IssueCoin
    {
        private IMockDBContext mockDBContext;

        private string privateRSA4096Key = @"-----BEGIN RSA PRIVATE KEY-----
MIIJKQIBAAKCAgEAo4jmIqFzPu8yp285exJffgsGmGA9jvvuWvEof69gKJSnpnv4
rSn8rd6Y+GngTPELLhar/kFKXnLN3VnTo21+tkB3aI4tKr2Fz5OjUL93BCwVGKDH
mInPMImo7xuM83LU4bGquRCBdbgYeeTltypXXnGoQS8KIzJIASYc9rh1nyCjxTQ0
URDbH69cQwJYjkfoCEtqvTeiueavcIxVm6aqo03fkPmev9a8JO1Gt8xbaVgNfg2S
Q7YceJaewUz3lMXuKxeoLD3g6sepGir/XG+Nzajpgjso71z8mZIIurk/bXAt/EAp
SU4BP+MJ1Bl7JFp/1dGfoueJr8z+r4NIUPzVTv3MsWXxb+6+bUbebB8Ez7p2lukn
LyttlwLkteF5y0WDCO68TN7MCLOI1kyU8YKr7/CZBqxEO1F9lWRhpfnsz5nbl9wq
4iHo4M+Q4ebS5+2gZvgXLiy3l5oyJX7bYcgPJm9JmuasLPJUvKc96OnAGBrUERu3
GCZ29HTzQrE95cMAA7uv10fnONX7Vs6a+zgnWFCiM0SPhkRRJf+gh6VQpPpUaq17
2RMEOD4wQw/qzI2w5ivOzvfi66UU/UOI/1yfdzqQU1j2oz2QeRl4DrEbS9MuyuYk
5zYvFajZGl3jtDVRQFE0Js7Fhvrv63kFOuQcLr50gsZmqmHRbAo8U0QqdZsCAwEA
AQKCAgEAop3SQuOK5y0SkL6gSQqnHH44S7N/4zwP9CzUmcu58gCIiZvDV+ZlljvA
n10hJ0GWXXk8BHPGUQ8WOSNGub96/h4y3yxqK5MUiaqE/xm1bterDrdqYvu+pMRu
0X3y9m7c06VJYAfSxE2RQTzlVPNst8SLjG4LRZ6NTV8p9AjPJbJeSA42a9o4G1TU
iOm/R5SnXgAonwnA7d0/3coeos3j5sncI5ylSJxusMWlycUTrThbClrSdDSWMS+N
pCg2FaGQyRcYKw5JJw3t0NCq568iMjziHpdXiFZN9SPrPEpTkS64+5qNs5A22+GX
d2rzMd4wOl3dwS8+kitlowAIHEiejvv+nLpaL4JVOAYfuMaEBYrs+1gUSy66GtAv
r5xwnWTdc2GctSZ3NyFeIDXBnBbqI/5V832zhuGSACJBEnHxc/ZSPWBJtjVpzeM7
s8q6o1cCKakhWwj4xW54Gf+8KTY4cLEivgZgPlw/gRQeX0UQVXGEqgsRy/2uXQGj
nzbhnG+SCTcN6xlBjddB+AvMYN16tbfutiB2OZDRlJ8qA3DeXJBd/aPSDQJKDn5K
7xfFp1sGWH0qX143+CRJjZVXpQBOCMKqUWBnCP1k5o31sK5SQJHJQhk8pdZWXgYg
z/5TMsVamMunzdejamnjjb/a1zBYStM4RQDzJyh6nXW6HroexekCggEBANCpHKjM
UNlP+bu3mtDwP8UT6KCT7zk4DuwD58yYYduuWUfheq2pk4cNRvnA+dH7OP6kFEO5
jb30mfg2ihgHOSOMg4GFiXZ3FaFlehPUKjdSGSHRLNQE6luLepyMUiS8s8vHPfNH
Ezj1a3mAUX66XlafrfefBOsjdMPbDgDni4oZymYMJyPavgKhvv/6KoweQe06DvTl
2Wl6ehrwxM7pWNtYprZNuInN6+VN+sOr4H03g9Wynsu+y0Tq5czqm/e8f/ReGgji
G09IJ0ZpwoqmNOSharbkXUqF95pxhD9NRInEPvaG9K80gR52Oxw1apPw6+ZwlGdi
LV/iDCMDRYyM15cCggEBAMii54vkmbm7Q02m6Ahs6I1yJjBo+BImCFRujHy4DEfW
G3FR/ENGGKtJFmOTJ+5S13YYb3Uov5pHxTs5vSgVTvgK3NyQgk0AY002UXF6gxSa
/Ro9bOGICLWIkEJNhHpxMMyLwkKDQTd/Q6a5lUiGqxxXohuhGZv9uQlNB5AkX0Du
RBPzYj0x4FcRnVDB8YA9uqhCaywTTkIurZ5qYsbPI44ZgZg4TUsJGMOV0kxgXOCF
8NLeLb4Q/JOWHXRP2naiX5wSQXEDVOGrWNdBEkyCtZDuVUwmOB1mN8nMZYTGvdMK
P8DhHQ3rd3gJvDpp6S6itOkHOd8I2ylIAgdNdoWfcp0CggEAR1C1Ud/a9hsGcKkV
CbpDIVlnlZJaHcVSHEpNbDjcoVeafh3LbxVZLbxU+MGC4MirNyTfJDKEBltWUGZb
llDJr6Ozwo3gaJPU2b+0FJjcAOJXYp56YtB3ROiL9HtlC9dO4CPtwXsWT85ZeEJq
GBUcbyDONaas6KzKmLICvD5nC8E60tvueKuna4DapeRbrF4fDDRerkbsosdQemq1
6T4Jt5Y6DG7N1pOBv3KCdWQGKuXRcjVFXr+L+7cI6Zt5yRcs6FlNLqIQ0W0It2Eo
M3kQ9N9SeMXeOL1G3gtUNspy1V80Yc/ISyV3x/CCzWZYUc3y4mSx5A/DEFVS7piM
qbftawKCAQBfPm4ay7dqIwptJ2mI7mJrGyAj5QC2ZNWZdy0724cA5xP59IiDSxhU
34pAVNAk47CiwDDQ9joWvDFVzzALgioNt+Jm1jb05NU/ByUccKMfOgAi37v7SD+S
JbUNtIzKL0eSIlpihrqD5Ocxk3HZjwxVlQhAg3aLxaN8VdPcSlfpk/I/Yk9la3Lr
AlKizp6dWTBtxbHop3WEF3KV/DzN0fE3+MW7hCa1EyltV4cQeMI+V3ceMxiV3Kx6
R03ONEYqASTQCx6DzqgRxOyUqrBMH9Sfa3SUhKQo2KhtJJmgsVPVhk0DfBn1zKzX
EBAJztNCLz0gTizbwvF/JE7KNqyn/CxFAoIBAQCdnNnlgmIZxgiqhQRkReQvHvMf
aVqxLm0yk7Bt/uQwklpsr964SLdZ4hje7OgrVkQmsaEntSTk+cSZv2Z1iej+fo41
Q0gPavY2Hy/+WXBV8IPBpUuUu1vWGn2kYzcxZJQ4K3jPCPlD4Tk8DrBvx49R+u9X
Wq7LXt1OqXw59S0D/tIoVT7fzBHMYD3AES0QYCzsYGkPwyz9ovG3OPrnimFzZMIb
HSYt4WstX3c5dDdgQqobESkKpaWurcOvU9PmuzogLDs7xb5lFbQyY6NwwTacFWeI
RQTnU21zY1AZOp31NclnqjQUow4KuMwIPWxXyBJLFXKl/YE+GABx6N/poqjg
-----END RSA PRIVATE KEY-----";

        private string publicRSA4096Key = @"-----BEGIN PUBLIC KEY-----
MIGeMA0GCSqGSIb3DQEBAQUAA4GMADCBiAKBgHOJWqrAUE45Dhk5gwJlFOLt6hKK
8+qpCriM7luk6Pk8u9AJpSDGip1pn0rFHdIRWWERMBE0EkHfH437TgAN7s42xcOH
JtIIB2MJlpA22vrP3xA1tr09UVH9Ig8SV6cMam+4Lxqn5tIKRSlXvdW+H2MYrn5N
JaFJqnhcmKVdKu2PAgMBAAE=
-----END PUBLIC KEY-----";

        public IssueCoin()
        {
            this.mockDBContext = new MockDBContext();
        }

        public Coin Create(string issuingAuthority, string countryCode, decimal value)
        {
            Coin coin = new Coin() { CountryCode = countryCode, IssueDate = DateTime.UtcNow, IssuingAuthority = issuingAuthority, SerialNumber = Guid.NewGuid(), Value = value, Holders = new List<Holder>() };
            

            string jCoin = JsonConvert.SerializeObject(coin);
            coin.IssuingHash = CoinTools.Sign(jCoin, privateRSA4096Key);

            coin.Holders.Add(new Holder() { PublicKey = publicRSA4096Key });
            jCoin = JsonConvert.SerializeObject(coin);
            coin.HolderHash = CoinTools.Sign(jCoin, privateRSA4096Key);

            mockDBContext.AddCoin(coin);

            return coin;
        }
    }
}
