using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClientMonitor.Application.Domanes.Response.Metrika
{
    /// <summary>
    /// Респонс яндекс метрики
    /// </summary>
    public class GetDataByTimeResponse
    {
        [JsonConstructor]
        public GetDataByTimeResponse(QueryData query, IReadOnlyList<DataData> data, string totalRows, string totalRowsRounded, bool sampled, bool contains, 
            double sampleShare, long sampleSize, long sampleSpace, int dataLag, IReadOnlyList<double> totals, IReadOnlyList<double> min, IReadOnlyList<double> max)
        {
            Query = query;
            Data = data;
            TotalRows = totalRows;
            TotalRowsRounded = totalRowsRounded;
            Sampled = sampled;
            Contains = contains;
            SampleShare = sampleShare;
            SampleSize = sampleSize;
            SampleSpace = sampleSpace;
            DataLag = dataLag;
            Totals = totals;
            Min = min;
            Max = max;
        }

        [JsonProperty("query")]
        public QueryData Query { get; }
        [JsonProperty("data")]
        public IReadOnlyList<DataData> Data { get; }
        [JsonProperty("total_rows")]
        public string TotalRows { get; }
        [JsonProperty("total_rows_rounded")]
        public string TotalRowsRounded { get; }
        [JsonProperty("sampled")]
        public bool Sampled { get; }
        [JsonProperty("contains_sensitive_data")]
        public bool Contains { get; }
        [JsonProperty("sample_share")]
        public double SampleShare { get; }
        [JsonProperty("sample_size")]
        public long SampleSize { get; }
        [JsonProperty("sample_space")]
        public long SampleSpace { get; }
        [JsonProperty("data_lag")]
        public int DataLag { get; }
        [JsonProperty("totals")]
        public IReadOnlyList<double> Totals { get; }
        [JsonProperty("min")]
        public IReadOnlyList<double> Min { get; }
        [JsonProperty("max")]
        public IReadOnlyList<double> Max { get; }
    }

    public class DataData
    {
        [JsonConstructor]
        public DataData(IReadOnlyList<string> dimensions, IReadOnlyList<int> metrics)
        {
            Dimensions = dimensions;
            Metrics = metrics;
        }

        [JsonProperty("dimensions")]
        public IReadOnlyList<string> Dimensions { get; }
        [JsonProperty("metrics")]
        public IReadOnlyList<int> Metrics { get; }
    }

    public class QueryData
    {
        [JsonConstructor]
        public QueryData(IReadOnlyList<int> ids, IReadOnlyList<string> dimensions, IReadOnlyList<string> metrics, IReadOnlyList<string> sort, string date1, string date2, int limit, 
            int offset, string group, int autoGroupSize, string attrName, int quantile, int offlineWindow, string attribution, string currency, 
            long adfoxEventId)
        {
            Ids = ids;
            Dimensions = dimensions;
            Metrics = metrics;
            Sort = sort;
            Date1 = date1;
            Date2 = date2;
            Limit = limit;
            Offset = offset;
            Group = group;
            AutoGroupSize = autoGroupSize;
            AttrName = attrName;
            Quantile = quantile;
            OfflineWindow = offlineWindow;
            Attribution = attribution;
            Currency = currency;
            AdfoxEventId = adfoxEventId;
        }

        [JsonProperty("ids")]
        public IReadOnlyList<int> Ids { get; }
        [JsonProperty("dimensions")]
        public IReadOnlyList<string> Dimensions { get; }
        [JsonProperty("metrics")]
        public IReadOnlyList<string> Metrics { get; }
        [JsonProperty("sort")]
        public IReadOnlyList<string> Sort { get; }
        [JsonProperty("date1")]
        public string Date1 { get; }
        [JsonProperty("date2")]
        public string Date2 { get; }
        [JsonProperty("limit")]
        public int Limit { get; }
        [JsonProperty("offset")]
        public int Offset { get; }
        [JsonProperty("group")]
        public string Group { get; }
        [JsonProperty("auto_group_size")]
        public int AutoGroupSize { get; }
        [JsonProperty("attr_name")]
        public string AttrName { get; }
        [JsonProperty("quantile")]
        public int Quantile { get; }
        [JsonProperty("offline_window")]
        public int OfflineWindow { get; }
        [JsonProperty("attribution")]
        public string Attribution { get; }
        [JsonProperty("currency")]
        public string Currency { get; }
        [JsonProperty("adfox_event_id")]
        public long AdfoxEventId { get; }
    }
}
