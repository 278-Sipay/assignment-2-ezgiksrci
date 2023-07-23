using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data.Domain;
using SipayApi.Data.Repository;
using SipayApi.Schema;

namespace SipayApi.Service;



[ApiController]
[Route("sipy/api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository repository;
    private readonly IMapper mapper;
    public TransactionController(ITransactionRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    [HttpGet]
    public ApiResponse<List<TransactionResponse>> GetAll()
    {
        var entityList = repository.GetAll();
        var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
        return new ApiResponse<List<TransactionResponse>>(mapped);
    }

    //====================================================================================================
    [HttpGet("GetByParameters")]
    public ApiResponse<List<TransactionResponse>> GetByParameters(int accountNumber, decimal? minCreditAmount, decimal? maxCreditAmount,
                                                                 decimal? minDebitAmount, decimal? maxDebitAmount, string description,
                                                                 DateTime? beginDate, DateTime? endDate, string referenceNumber)
    {
        var entityList = repository.GetByParameter(accountNumber, minCreditAmount, maxCreditAmount,
                                                    minDebitAmount, maxDebitAmount, description,
                                                    beginDate, endDate, referenceNumber);

        var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
        return new ApiResponse<List<TransactionResponse>>(mapped);
    }



}
