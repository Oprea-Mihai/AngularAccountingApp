const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:27868';

//const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :

//env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7067';

const PROXY_CONFIG = [
  {
    context: [
      "/api/payment/unpaid",
      "/api/payment/lastpayment",
      "/api/payment/updatepayment",
      "/api/project/budget",
      "/api/project/updateprojectbudget",
      "/api/excel/post",
      "/api/excel/checkNewEmp",
      "/api/excel/validateExcel",
      "/api/home/getWeeks",
      "/api/paymenthistory/toselect",
      "/api/paymenthistory",
      "/api/projecthistory/toselect",
      "/api/projecthistory",
      "/api/project/unfinishedProjects",
      "/api/project/markFinishedProject"
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
