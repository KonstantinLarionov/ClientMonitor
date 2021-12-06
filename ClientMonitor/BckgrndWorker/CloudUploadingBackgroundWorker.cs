﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class CloudUploadingBackgroundWorker : BackgroundService
    {
        readonly ICludUploadHendler _handle;
        readonly IRepository<DataForEditInfo> _db;
        private static bool isEnable = false;
        public CloudUploadingBackgroundWorker(ICludUploadHendler handle, IRepository<DataForEditInfo> db)
        {
            _handle = handle;
            _db = db;
        }

        /// <summary>
        /// Запуск службы загрузки в облако
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var repository = _db;
                if (repository.GetData("onOff") != "")
                {
                    isEnable = ParseBool(repository.GetData("onOff"));
                }
                if (isEnable == false)
                {
                    int dt = 18;
                    //получение времени с БД
                    if (repository.GetData("TimeCloud") != "")
                    {
                        dt = Convert.ToDateTime(repository.GetData("TimeCloud")).Hour;
                    }
                    //if (dt == DateTime.Now.Hour)
                    //{
                        await _handle.Handle();
                        Thread.Sleep(85800000);
                    //}
                    //else
                    //{
                    //    Thread.Sleep(10000);
                    //}
                }
                else
                {
                    Thread.Sleep(10000);
                }
                await Task.Delay(1000, stoppingToken);
            }

            //return Task.CompletedTask;
        }

        /// <summary>
        /// Конвертирование string в bool
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ParseBool(string input)
        {
            if (input == "True")
                return true;
            if (input == "False")
                return false;
            else return false;
        }
    }
}
