/*************************************************************************
Copyright 2008 by Sergey Bochkanov (ALGLIB project).

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above copyright
  notice, this list of conditions and the following disclaimer listed
  in this license in the documentation and/or other materials
  provided with the distribution.

- Neither the name of the copyright holders nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*************************************************************************/

using System;
public partial class alglib
{
    public class bayes
    {
        public struct naivebayesmodel
        {
            public double[] b;
        };
        public struct nbcreport
        {
            public double relclserror;
            public double avgce;
            public double cvavgce;
        };


        public const int nbcvnum = 4;


        /*************************************************************************
        Создание наивного байесовского классификатора

        ВХОДНЫЕ ПАРАМЕТРЫ:
            XY          -   обучающая выборка, array [0..NPoints-1, 0..NVars].
                            формат выборки описан ниже.
            M           -   маска пропущенных значений,
                            array[0..NPoints-1, 0..NVars-1]
                            Если M[I,J] равно True, то значение XY[I,J] ПРОПУЩЕНО.
            NPoints     -   число элементов в выборке, NPoints>=0
            F           -   array[0...NVars-1], массив, содержащий число возможных
                            значений каждой переменной: F[i]>0 обозначает номинальную
                            переменную,  принимающую F[i] разных значений (от 0 до
                            F[i]-1),  а  специальное  значение  F[i]=0  обозначает
                            вещественную переменную.
            NVars       -   число переменных, NVars>=1
            NClasses    -   число классов, NClasses>=1
            Flags       -   флаги. Параметр равен сумме следующих значений:
                            * 1, если  требуется  использовать  более   трудоемкий
                                 алгоритм оптимальной дискретизации  вместо  менее
                                 трудоемкого неоптимального  алгоритма  (см. ниже,
                                 раздел ДИСКРЕТИЗАЦИЯ).

        ВЫХОДНЫЕ ПАРАМЕТРЫ:
            Info        -   код завершения работы, равен:
                            * -3, в случае, если F[i]<0 (неверное число значений для
                                  i-ой переменной)
                            * -2, если один из элементов XY имеет неверный номер
                                  переменной (за пределами диапазона, определенного
                                  массивом F) или неверный номер класса.
                            * -1, в случае, если переданые неверные параметры (NPoints<0,
                                  NVars<1, NClasses<1).
                            *  1, в случае успешного завершения
            B           -   модель во внутреннем формате ALGLIB.
            Rep         -   отчет о работе, содержащий дополнительную информацию

        ФОРМАТ ВЫБОРКИ

        Выборка передается в массиве XY, имеющем NPoints строк и NVars+1 столбцов.
        В   первых   NVars   столбцах  располагаются   значения  переменных,  J-ая
        переменная может принимать целочисленные значения  в  диапазоне 0...F[J]-1
        (в случае номинальной переменной)  или  произвольные вещественные значения
        (в  случае   вещественной   переменной).  В  последнем  столбце  находится
        номер   класса,   к   которому   относится   элемент   выборки,   диапазон
        значений - 0..NClasses-1.


        ДИСКРЕТИЗАЦИЯ

        Для работы Байесовского классификатора все вещественные переменные  должны
        быть  дискретизированы.   Дискретизация   осуществляется   прозрачно   для
        пользователя, следует только выбрать алгоритм дискретизации.  Пакет ALGLIB
        содержит два алгоритма: неоптимальный, обладающий трудоемкостью O(NPoints^
        1.33), и оптимальный, трудоемкость которого равна O(NVars*NPoints^2.33).
        Оптимальный  алгоритм  находит  разбиение  диапазона значений вещественной
        переменной на интервалы, при котором минимизируется ошибка классификации с
        использованием только этой переменной. Неоптимальный алгоритм  также  ищет
        минимум, но только среди разбиений  на  интервалы,  содержащие  одинаковое
        число значений переменной.
        Оптимальный  алгоритм  лучше  адаптируется  к  особенностям распределения,
        размещая границы интервалов как можно ближе к границам классов.  Однако  с
        ростом размера выборки это преимущество становится менее значительным, т.к.
        возрастает исло интервалов, на которые можно разбить диапазон значений,  а
        значит, и неоптимальный алгоритм становится достаточно гибок.
        Выбор алгоритма главным образом определяется соображениями  быстродействия
        - если вы можете позволить себе использовать оптимальный алгоритм,  почему
        бы не использовать его?  Если  нет  (например,  размер  выборки измеряется
        десятками тысяч элементов), то остается только неоптимальный алгоритм.


        ПРОПУЩЕННЫЕ ЗНАЧЕНИЯ

        Часть значений независимых переменных может быть пропущена. Такие значения
        указываются в массиве-маске M. Если M[I,J] равно True, то значение XY[I,J]
        пропущено. Только независимые переменные могут быть пропущены. Номер класса
        не может быть пропущен.


          -- ALGLIB --
             Copyright 18.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static void nbcbuildm(ref double[,] xy,
            ref bool[,] m,
            int npoints,
            ref int[] f,
            int nvars,
            int nclasses,
            int flags,
            ref int info,
            ref double[] b,
            ref nbcreport rep)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int kmax = 0;
            int nnotmissing = 0;
            int[] c = new int[0];
            int[] fi = new int[0];
            double[] x = new double[0];
            int[,] ixy = new int[0, 0];
            double[,] thresholds = new double[0, 0];
            double[] t = new double[0];
            double t2 = 0;
            int ni = 0;
            int ni2 = 0;
            double cvk = 0;
            double cv2 = 0;
            double cvbest = 0;
            int alen = 0;
            int half = 0;
            int first = 0;
            int middle = 0;
            int splitinfo = 0;
            int curf = 0;
            double tmp1 = 0;
            double tmp2 = 0;
            double tmp3 = 0;
            double tmp4 = 0;
            bool optimalsplit = new bool();
            int i_ = 0;


            //
            // Test parameters
            //
            if (npoints < 0 | nvars < 1 | nclasses < 2 | flags != 0 & flags != 1)
            {
                info = -1;
                return;
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                if (f[i] < 0)
                {
                    info = -3;
                    return;
                }
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (f[j] > 0 & !m[i, j] & ((int)Math.Round(xy[i, j]) >= f[j] | (int)Math.Round(xy[i, j]) < 0))
                    {
                        info = -2;
                        return;
                    }
                }
                if ((int)Math.Round(xy[i, nvars]) < 0 | (int)Math.Round(xy[i, nvars]) >= nclasses)
                {
                    info = -2;
                    return;
                }
            }
            info = 1;
            optimalsplit = flags == 1;

            //
            // convert from real to integer
            //
            if (npoints > 0)
            {

                //
                // Calculate thresholds and convert
                //
                kmax = Math.Max((int)Math.Ceiling(2 + Math.Pow(npoints, 0.333)), nclasses);
                thresholds = new double[nvars - 1 + 1, kmax - 2 + 1];
                ixy = new int[npoints - 1 + 1, nvars + 1];
                x = new double[npoints - 1 + 1];
                c = new int[npoints - 1 + 1];
                fi = new int[nvars - 1 + 1];
                for (i = 0; i <= npoints - 1; i++)
                {
                    ixy[i, nvars] = (int)Math.Round(xy[i, nvars]);
                }
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (f[j] > 0)
                    {

                        //
                        // integer variable: just copy
                        //
                        for (i = 0; i <= npoints - 1; i++)
                        {
                            if (!m[i, j])
                            {
                                ixy[i, j] = (int)Math.Round(xy[i, j]);
                            }
                            else
                            {
                                ixy[i, j] = -1;
                            }
                        }
                        fi[j] = f[j];
                    }
                    else
                    {

                        //
                        // real variable: split and convert
                        // 0.A prepare "no-split" solution (in case everything fails)
                        //
                        cvbest = math.maxrealnumber;
                        fi[j] = 1;
                        for (i = 0; i <= npoints - 1; i++)
                        {
                            ixy[i, j] = 0;
                        }

                        //
                        // 0.B skip missing values
                        //
                        nnotmissing = 0;
                        for (i = 0; i <= npoints - 1; i++)
                        {
                            if (!m[i, j])
                            {
                                x[nnotmissing] = xy[i, j];
                                c[nnotmissing] = (int)Math.Round(xy[i, nvars]);
                                nnotmissing = nnotmissing + 1;
                            }
                        }

                        //
                        // If there is at least one non-missing value
                        //
                        if (nnotmissing > 0)
                        {

                            //
                            // 1. try optimal 2-split (if only two classes)
                            //
                            if (nclasses == 2)
                            {
                                bdss.dsoptimalsplit2(x, c, nnotmissing, ref splitinfo, ref t2, ref tmp1, ref tmp2, ref tmp3, ref tmp4, ref cv2);
                                if (splitinfo == 1 & cv2 < cvbest)
                                {
                                    fi[j] = 2;
                                    thresholds[j, 0] = t2;
                                    for (i = 0; i <= npoints - 1; i++)
                                    {
                                        if (!m[i, j])
                                        {
                                            if (xy[i, j] < t2)
                                            {
                                                ixy[i, j] = 0;
                                            }
                                            else
                                            {
                                                ixy[i, j] = 1;
                                            }
                                        }
                                        else
                                        {
                                            ixy[i, j] = -1;
                                        }
                                    }
                                }
                            }

                            //
                            // 2. try k-split
                            //
                            if (optimalsplit)
                            {
                                bdss.dsoptimalsplitk(x, c, nnotmissing, nclasses, kmax, ref splitinfo, ref t, ref ni, ref cvk);
                            }
                            else
                            {
                                bdss.dssplitk(x, c, nnotmissing, nclasses, kmax, ref splitinfo, ref t, ref ni, ref cvk);
                            }
                            if (splitinfo == 1 & cvk < cvbest)
                            {
                                fi[j] = ni;
                                for (i_ = 0; i_ <= ni - 2; i_++)
                                {
                                    thresholds[j, i_] = t[i_];
                                }
                                for (i = 0; i <= npoints - 1; i++)
                                {
                                    if (!m[i, j])
                                    {

                                        //
                                        // non-missing value
                                        //
                                        alen = ni - 1;
                                        first = 0;
                                        while (alen > 0)
                                        {
                                            half = alen / 2;
                                            middle = first + half;
                                            if (xy[i, j] < t[middle])
                                            {
                                                alen = half;
                                            }
                                            else
                                            {
                                                first = middle + 1;
                                                alen = alen - half - 1;
                                            }
                                        }
                                        ixy[i, j] = first;
                                    }
                                    else
                                    {
                                        ixy[i, j] = -1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //
            // Build discrete bayes classifier
            //
            nbcbuildi(ref ixy, npoints, ref fi, nvars, nclasses, ref info, ref b, ref rep);
            if (info > 0)
            {

                //
                // Correct thresholds, if needed
                //
                curf = (int)Math.Round(b[5]);
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (f[j] == 0)
                    {
                        for (k = 0; k <= fi[j] - 2; k++)
                        {
                            b[curf + k + 1] = thresholds[j, k];
                        }
                    }
                    curf = curf + (int)Math.Round(b[curf]);
                }
            }
        }


        /*************************************************************************
        Создание наивного байесовского классификатора.

        Вариант подпрограммы, не принимающий маску пропущенных значений (пропущенные
        значения отсутствуют).

        Все параметры подпрограммы аналогичны подобным у NBCBuildM.

          -- ALGLIB --
             Copyright 17.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static void nbcbuild(ref double[,] xy,
            int npoints,
            ref int[] f,
            int nvars,
            int nclasses,
            int flags,
            ref int info,
            ref double[] b,
            ref nbcreport rep)
        {
            int i = 0;
            int j = 0;
            bool[,] m = new bool[0, 0];


            //
            // Test parameters: only partial test.
            // Full test is performed by NBCBuildM
            //
            if (npoints < 0 | nvars < 1 | nclasses < 2)
            {
                info = -1;
                return;
            }
            if (npoints > 0)
            {
                m = new bool[npoints - 1 + 1, nvars - 1 + 1];
                for (i = 0; i <= npoints - 1; i++)
                {
                    for (j = 0; j <= nvars - 1; j++)
                    {
                        m[i, j] = false;
                    }
                }
            }
            nbcbuildm(ref xy, ref m, npoints, ref f, nvars, nclasses, flags, ref info, ref b, ref rep);
        }


        /*************************************************************************
        Создание наивного байесовского классификатора: дискретный вариант.

        Значение  параметров  подпрограммы  то  же, что  и  в  случае NBCBuild, за
        следующими исключениями:
        * массив XY (выборка) имеет целый, а не вещественный тип
        * элементы массива F, содержащего информацию о типе переменной,  принимают
          строго положительные значения (только номинальные переменные).
        * маска пропущенных значений отсуствует. Однако можно сообщить, что то или
          иное значение пропущено, указав -1 вместо числа  в  диапазоне  0..F[J]-1
          (отрицательное значение номинальной переменной обозначает пропуск).
        * кросс-валидационная оценка ошибки всегда точна

          -- ALGLIB --
             Copyright 01.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static void nbcbuildi(ref int[,] xy,
            int npoints,
            ref int[] f,
            int nvars,
            int nclasses,
            ref int info,
            ref double[] b,
            ref nbcreport rep)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int cur = 0;
            double s = 0;
            double v = 0;
            int headersize = 0;
            int bsize = 0;
            int[,] cfc = new int[0, 0];
            int[,] sfc = new int[0, 0];
            int[] cc = new int[0];
            int sc = 0;
            double[,] pfc = new double[0, 0];
            int[] foffsets = new int[0];
            int regconst = 0;
            int finfosize = 0;
            int pinfosize = 0;
            int cinfosize = 0;
            int bufsize = 0;
            int vinfooffset = 0;
            int pinfooffset = 0;
            int cinfooffset = 0;
            int bufoffset = 0;
            double[] tpc = new double[0];
            double[] cvpc = new double[0];
            int mx = 0;
            int i_ = 0;
            int i1_ = 0;


            //
            // Test parameters
            //
            if (npoints < 0 | nvars < 1 | nclasses < 2)
            {
                info = -1;
                return;
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                if (f[i] < 1)
                {
                    info = -3;
                    return;
                }
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (xy[i, j] >= f[j])
                    {
                        info = -2;
                        return;
                    }
                }
                if (xy[i, nvars] < 0 | xy[i, nvars] >= nclasses)
                {
                    info = -2;
                    return;
                }
            }
            info = 1;

            //
            // ALLOCATE B
            //
            // B header:
            //
            // B[0]             -   sizeof(B)
            // B[1]             -   version ID
            // B[2]             -   vars count, NVars
            // B[3]             -   classes count, NClasses
            // B[4]             -   regularization constant
            // B[5]             -   vars information offset, FOffset
            // B[6]             -   priors offset, POffset
            // B[7]             -   conditional probabilities offset, COffset
            // B[8]             -   Process/ProcessM buffer offset
            //
            //
            // Vars information:
            //
            // B[VOffset+0]     -   var 1: values count, VCount
            // B[VOffset+1,...] -   thresholds, VCount-1 values
            // ...
            //
            //
            // Priors information:
            //
            // B[POffset+0]     -   class 0 prior probability
            // B[POffset+1]     -   class 1 prior probability
            // ...
            //
            //
            // Conditional probabilities:
            //
            // B[COffset+0]     -   class 0, variable 0, value 0
            // B[COffset+1]     -   class 1, variable 0, value 0
            // ...
            // B[COffset+..]    -   class NClasses-1, variable 0, value 0
            // B[COffset+..]    -   class 0, variable 0, value 1
            // B[COffset+..]    -   class 1, variable 0, value 1
            // ...
            //
            headersize = 9;
            finfosize = 0;
            for (i = 0; i <= nvars - 1; i++)
            {
                finfosize = finfosize + 1 + (f[i] - 1);
            }
            pinfosize = nclasses;
            cinfosize = 0;
            for (i = 0; i <= nvars - 1; i++)
            {
                cinfosize = cinfosize + nclasses * f[i];
            }
            bufsize = nvars + nclasses;
            vinfooffset = headersize;
            pinfooffset = vinfooffset + finfosize;
            cinfooffset = pinfooffset + pinfosize;
            bufoffset = cinfooffset + cinfosize;
            bsize = headersize + finfosize + pinfosize + cinfosize + bufsize;
            b = new double[bsize - 1 + 1];

            //
            // Header
            //
            regconst = 1;
            b[0] = bsize;
            b[1] = nbcvnum;
            b[2] = nvars;
            b[3] = nclasses;
            b[4] = regconst;
            b[5] = vinfooffset;
            b[6] = pinfooffset;
            b[7] = cinfooffset;
            b[8] = bufoffset;

            //
            // Fill FInfo
            //
            cur = vinfooffset;
            for (k = 0; k <= nvars - 1; k++)
            {
                b[cur] = f[k];
                for (j = 1; j <= f[k] - 1; j++)
                {
                    b[cur + j] = j - 0.5;
                }
                cur = cur + 1 + (f[k] - 1);
            }

            //
            // Fill PInfo
            //
            // Important variables initialized:
            // * PInfo section of B
            // * CC - CC[i] = size(I-th class)
            // * SC - sum(CC)
            //
            cc = new int[nclasses - 1 + 1];
            sc = 0;
            for (k = 0; k <= nclasses - 1; k++)
            {
                cc[k] = 0;
            }
            if (npoints != 0)
            {

                //
                // Priors as usual
                //
                for (k = 0; k <= nclasses - 1; k++)
                {
                    b[pinfooffset + k] = regconst;
                }
                for (i = 0; i <= npoints - 1; i++)
                {
                    b[pinfooffset + xy[i, nvars]] = b[pinfooffset + xy[i, nvars]] + 1;
                    cc[xy[i, nvars]] = cc[xy[i, nvars]] + 1;
                    sc = sc + 1;
                }
                s = 0;
                for (j = 0; j <= nclasses - 1; j++)
                {
                    s = s + b[pinfooffset + j];
                }
                v = 1 / s;
                for (i_ = pinfooffset; i_ <= pinfooffset + nclasses - 1; i_++)
                {
                    b[i_] = v * b[i_];
                }
            }
            else
            {

                //
                // Assume equiprobable classes if no info available.
                //
                for (j = 0; j <= nclasses - 1; j++)
                {
                    b[pinfooffset + j] = (double)(1) / (double)(nclasses);
                }
            }

            //
            // Fill CInfo:
            // 1. fill FOffsets (just for convenience)
            // 2. calculate CFC (conditional counts, Count(F|C))
            // 3. calculate PFC - regularized P(F|C)
            // 4. calculate SFC - sum(PFC|F)
            // 5. pack PFC
            //
            for (k = 0; k <= cinfosize - 1; k++)
            {
                b[cinfooffset + k] = 0;
            }
            foffsets = new int[nvars + 1];
            foffsets[0] = 0;
            for (i = 0; i <= nvars - 1; i++)
            {
                foffsets[i + 1] = foffsets[i] + f[i];
            }
            cfc = new int[foffsets[nvars] - 1 + 1, nclasses - 1 + 1];
            sfc = new int[nvars - 1 + 1, nclasses - 1 + 1];
            for (i = 0; i <= foffsets[nvars] - 1; i++)
            {
                for (j = 0; j <= nclasses - 1; j++)
                {
                    cfc[i, j] = 0;
                }
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (xy[i, j] >= 0)
                    {
                        cfc[foffsets[j] + xy[i, j], xy[i, nvars]] = cfc[foffsets[j] + xy[i, j], xy[i, nvars]] + 1;
                    }
                }
            }
            pfc = new double[foffsets[nvars] - 1 + 1, nclasses - 1 + 1];
            for (k = 0; k <= nvars - 1; k++)
            {
                for (j = 0; j <= nclasses - 1; j++)
                {
                    sfc[k, j] = 0;
                    for (i = foffsets[k]; i <= foffsets[k + 1] - 1; i++)
                    {
                        sfc[k, j] = sfc[k, j] + cfc[i, j];
                    }
                    for (i = foffsets[k]; i <= foffsets[k + 1] - 1; i++)
                    {
                        pfc[i, j] = ((double)(cfc[i, j] + regconst)) / ((double)(sfc[k, j] + regconst * f[k]));
                    }
                }
            }
            cur = cinfooffset;
            for (i = 0; i <= foffsets[nvars] - 1; i++)
            {
                i1_ = (0) - (cur);
                for (i_ = cur; i_ <= cur + nclasses - 1; i_++)
                {
                    b[i_] = pfc[i, i_ + i1_];
                }
                cur = cur + nclasses;
            }

            //
            // Fill report
            //
            cvpc = new double[nclasses - 1 + 1];
            tpc = new double[nclasses - 1 + 1];
            rep.relclserror = 0;
            rep.avgce = 0;
            rep.cvavgce = 0;
            for (i = 0; i <= npoints - 1; i++)
            {

                //
                // Obtain estimates (CV and usual) for the I-th example in the training set
                //
                for (j = 0; j <= nclasses - 1; j++)
                {
                    tpc[j] = b[pinfooffset + j];
                    if (j == xy[i, nvars])
                    {
                        cvpc[j] = ((double)(cc[j] - 1 + regconst)) / ((double)(sc - 1 + regconst * nclasses));
                    }
                    else
                    {
                        cvpc[j] = ((double)(cc[j] + regconst)) / ((double)(sc - 1 + regconst * nclasses));
                    }
                }
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (xy[i, j] >= 0)
                    {

                        //
                        // Update probabilities (CV/usual)
                        //
                        for (k = 0; k <= nclasses - 1; k++)
                        {
                            tpc[k] = tpc[k] * (cfc[foffsets[j] + xy[i, j], k] + regconst) / (sfc[j, k] + regconst * f[j]);
                            if (k == xy[i, nvars])
                            {
                                cvpc[k] = cvpc[k] * (cfc[foffsets[j] + xy[i, j], k] - 1 + regconst) / (sfc[j, k] - 1 + regconst * f[j]);
                            }
                            else
                            {
                                cvpc[k] = cvpc[k] * (cfc[foffsets[j] + xy[i, j], k] + regconst) / (sfc[j, k] + regconst * f[j]);
                            }
                        }

                        //
                        // Renormalize to avoid overflows
                        //
                        s = 0;
                        for (k = 0; k <= nclasses - 1; k++)
                        {
                            s = s + tpc[k];
                        }
                        v = 1 / s;
                        for (i_ = 0; i_ <= nclasses - 1; i_++)
                        {
                            tpc[i_] = v * tpc[i_];
                        }
                        s = 0;
                        for (k = 0; k <= nclasses - 1; k++)
                        {
                            s = s + cvpc[k];
                        }
                        v = 1 / s;
                        for (i_ = 0; i_ <= nclasses - 1; i_++)
                        {
                            cvpc[i_] = v * cvpc[i_];
                        }
                    }
                }

                //
                // Calculate errors
                //
                mx = 0;
                for (j = 1; j <= nclasses - 1; j++)
                {
                    if (tpc[j] > tpc[mx])
                    {
                        mx = j;
                    }
                }
                if (mx != xy[i, nvars])
                {
                    rep.relclserror = rep.relclserror + 1;
                }
                if (tpc[xy[i, nvars]] != 0)
                {
                    rep.avgce = rep.avgce - Math.Log(tpc[xy[i, nvars]]);
                }
                else
                {
                    rep.avgce = rep.avgce - Math.Log(math.minrealnumber);
                }
                if (cvpc[xy[i, nvars]] != 0)
                {
                    rep.cvavgce = rep.cvavgce - Math.Log(cvpc[xy[i, nvars]]);
                }
                else
                {
                    rep.cvavgce = rep.cvavgce - Math.Log(math.minrealnumber);
                }
            }
            rep.relclserror = rep.relclserror / npoints;
            rep.avgce = rep.avgce / (npoints * Math.Log(2));
            rep.cvavgce = rep.cvavgce / (npoints * Math.Log(2));
        }


        /*************************************************************************
        Получение  вектора  вероятностей  отнесения  элемента  выборки к одному из
        классов.

        Входные параметры:
            B       -   массив, задающий классификатор, построенный  на  основе
                        обучающей выборки.
            X       -   входной  вектор,   по  размеру  должен  быть  равен  числу
                        переменных. Содержит набор значений  переменных в формате,
                        описанном в комментариях к подпрограмме NBCBuild.
                        Вектор   является  массивом  вещественных  чисел. В случае,
                        если  переменная  является   номинальной,  соответствующая
                        компонента  равна  целому  числу  от  0  до  максимального
                        значения  переменной  (см.  комментарий   к   подпрограмме
                        NBCBuild).

        Выходные параметры:
            Y       -   результат  работы  классификатора  -  вектор  вероятностей
                        отнесения образа к одному из классов.
                        Подпрограмма не  выделяет  память  под  этот  вектор,  это
                        обязанность  того,  кто  вызывает  подпрограмму,  выделить
                        память под результат.

          -- ALGLIB --
             Copyright 11.07.2008 by Bochkanov Sergey
        *************************************************************************/
        public static void nbcprocess(ref double[] b,
            ref double[] x,
            ref double[] pc)
        {
            bool[] m = new bool[0];

            nbcprocessinternal(ref b, ref x, ref m, false, ref pc);
        }


        /*************************************************************************
        Получение  вектора  вероятностей  отнесения  элемента  выборки к одному из
        классов. Вариант подпрограммы, допускающий пропущенные значения.

        Входные параметры:
            B       -   массив, задающий классификатор, построенный  на  основе
                        обучающей выборки.
            X       -   входной  вектор,   по  размеру  должен  быть  равен  числу
                        переменных. Содержит набор значений переменных в  формате,
                        описанном в комментариях к подпрограмме NBCBuild.
                        Вектор   является  массивом  вещественных  чисел. В случае,
                        если  переменная  является   номинальной,  соответствующая
                        компонента  равна  целому  числу  от  0  до  максимального
                        значения  переменной  (см.  комментарий   к   подпрограмме
                        NBCBuild).
            M       -   вектор, задающий маску пропущенных значений. Если  элемент
                        вектора равен True, соответствующее значение ПРОПУЩЕНО.

        Выходные параметры:
            Y       -   результат  работы  классификатора  -  вектор  вероятностей
                        отнесения образа к одному из классов.
                        Подпрограмма не  выделяет  память  под  этот  вектор,  это
                        обязанность  того,  кто  вызывает  подпрограмму,  выделить
                        память под результат.

          -- ALGLIB --
             Copyright 11.07.2008 by Bochkanov Sergey
        *************************************************************************/
        public static void nbcprocessm(ref double[] b,
            ref double[] x,
            ref bool[] m,
            ref double[] pc)
        {
            nbcprocessinternal(ref b, ref x, ref m, true, ref pc);
        }


        /*************************************************************************
        Средняя кросс-энтропия (в битах на элемент множества).
        Формула  для  вычисления  имеет  следующий  вид (обозначая кросс-энтропию,
        как CE):

            AvgCE = CE/(Ln(2)*NPoints)

        Входные параметры:
            B       -   массив. Байесовский классификатор во внутреннем формате.
                        массив  передается  по  ссылке  и  модифицируется  в  ходе
                        работы   т.к.   его  элементы  используются  для  хранения
                        временных переменных.
            XY      -   тестовое множество.
                        массив размером [0..NPoints-1,0..NVars].
                        первые  NVars  столбцов содержат входные данные, последний
                        столбец  содержит  номер  класса  (от  0 до NClasses-1), к
                        которому принадлежит элемент выборки.
            NPoints -   размер тестового множества

        Результат:
            средняя кросс-энтропия в битах на элемент множества

          -- ALGLIB --
             Copyright 19.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static double nbcavgce(ref double[] b,
            ref double[,] xy,
            int npoints)
        {
            double result = 0;
            int nvars = 0;
            int nclasses = 0;
            int i = 0;
            int j = 0;
            double[] workx = new double[0];
            double[] worky = new double[0];
            int nmax = 0;
            int i_ = 0;

            System.Diagnostics.Debug.Assert((int)Math.Round(b[1]) == nbcvnum, "NBCAvgCE: incorrect classificator array!");
            nvars = (int)Math.Round(b[2]);
            nclasses = (int)Math.Round(b[3]);
            workx = new double[nvars - 1 + 1];
            worky = new double[nclasses - 1 + 1];
            result = 0;
            for (i = 0; i <= npoints - 1; i++)
            {
                System.Diagnostics.Debug.Assert((int)Math.Round(xy[i, nvars]) >= 0 & (int)Math.Round(xy[i, nvars]) < nclasses, "NBCAvgCE: incorrect class number!");

                //
                // Process
                //
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    workx[i_] = xy[i, i_];
                }
                nbcprocess(ref b, ref workx, ref worky);
                if (worky[(int)Math.Round(xy[i, nvars])] > 0)
                {
                    result = result - Math.Log(worky[(int)Math.Round(xy[i, nvars])]);
                }
                else
                {
                    result = result - Math.Log(math.minrealnumber);
                }
            }
            result = result / (npoints * Math.Log(2));
            return result;
        }


        /*************************************************************************
        Аналог подпрограммы NBCAvgCE, принимающий маску пропущенных значений.

        Параметр M - маска пропущенных значений.
        array[0..NPoints-1,0..NVars-1]
        Если M[I,J] равно True, то значение XY[I,J] пропущено.

        Прочие параметры аналогичны подпрограмме NBCAvgCE

          -- ALGLIB --
             Copyright 19.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static double nbcavgcem(ref double[] b,
            ref double[,] xy,
            ref bool[,] m,
            int npoints)
        {
            double result = 0;
            int nvars = 0;
            int nclasses = 0;
            int i = 0;
            int j = 0;
            double[] workx = new double[0];
            double[] worky = new double[0];
            bool[] mx = new bool[0];
            int nmax = 0;
            int i_ = 0;

            System.Diagnostics.Debug.Assert((int)Math.Round(b[1]) == nbcvnum, "NBCAvgCEM: incorrect classificator array!");
            nvars = (int)Math.Round(b[2]);
            nclasses = (int)Math.Round(b[3]);
            workx = new double[nvars - 1 + 1];
            mx = new bool[nvars - 1 + 1];
            worky = new double[nclasses - 1 + 1];
            result = 0;
            for (i = 0; i <= npoints - 1; i++)
            {
                System.Diagnostics.Debug.Assert((int)Math.Round(xy[i, nvars]) >= 0 & (int)Math.Round(xy[i, nvars]) < nclasses, "NBCAvgCEM: incorrect class number!");

                //
                // Process
                //
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    workx[i_] = xy[i, i_];
                }
                for (j = 0; j <= nvars - 1; j++)
                {
                    mx[j] = m[i, j];
                }
                nbcprocessm(ref b, ref workx, ref mx, ref worky);
                if (worky[(int)Math.Round(xy[i, nvars])] > 0)
                {
                    result = result - Math.Log(worky[(int)Math.Round(xy[i, nvars])]);
                }
                else
                {
                    result = result - Math.Log(math.minrealnumber);
                }
            }
            result = result / (npoints * Math.Log(2));
            return result;
        }


        /*************************************************************************
        Получение относительной ошибки классификации на тестовом множестве


          -- ALGLIB --
             Copyright 14.01.2009 by Bochkanov Sergey
        *************************************************************************/
        public static double nbcrelclserror(ref double[] b,
            ref double[,] xy,
            int npoints)
        {
            double result = 0;

            result = (double)(nbcclserror(ref b, ref xy, npoints)) / (double)(npoints);
            return result;
        }


        /*************************************************************************
        Получение относительной ошибки классификации на тестовом множестве
        (вариант, поддерживающий пропущенные значения)

          -- ALGLIB --
             Copyright 14.01.2009 by Bochkanov Sergey
        *************************************************************************/
        public static double nbcrelclserrorm(ref double[] b,
            ref double[,] xy,
            ref bool[,] m,
            int npoints)
        {
            double result = 0;

            result = (double)(nbcclserrorm(ref b, ref xy, ref m, npoints)) / (double)(npoints);
            return result;
        }


        /*************************************************************************
        Получение ошибки классификации на тестовом множестве

        Входные параметры: аналогично подпрограмме NBCAvgCE

        Результат:
            ошибка классификации (число неправильно классифицированных случаев)

          -- ALGLIB --
             Copyright 19.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static int nbcclserror(ref double[] b,
            ref double[,] xy,
            int npoints)
        {
            int result = 0;
            int nvars = 0;
            int nclasses = 0;
            int i = 0;
            int j = 0;
            double[] workx = new double[0];
            double[] worky = new double[0];
            int nmax = 0;
            int i_ = 0;

            System.Diagnostics.Debug.Assert((int)Math.Round(b[1]) == nbcvnum, "NBCClsError: incorrect classificator array!");
            nvars = (int)Math.Round(b[2]);
            nclasses = (int)Math.Round(b[3]);
            workx = new double[nvars - 1 + 1];
            worky = new double[nclasses - 1 + 1];
            result = 0;
            for (i = 0; i <= npoints - 1; i++)
            {

                //
                // Process
                //
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    workx[i_] = xy[i, i_];
                }
                nbcprocess(ref b, ref workx, ref worky);

                //
                // NBC version of the answer
                //
                nmax = 0;
                for (j = 0; j <= nclasses - 1; j++)
                {
                    if (worky[j] > worky[nmax])
                    {
                        nmax = j;
                    }
                }

                //
                // compare
                //
                if (nmax != (int)Math.Round(xy[i, nvars]))
                {
                    result = result + 1;
                }
            }
            return result;
        }


        /*************************************************************************
        Аналог подпрограммы NBCClsError, принимающий маску пропущенных значений.

        Параметр M - маска пропущенных значений.
        array[0..NPoints-1,0..NVars-1]
        Если M[I,J] равно True, то значение XY[I,J] пропущено.

        Прочие параметры аналогичны подпрограмме NBCClsError

          -- ALGLIB --
             Copyright 19.10.2008 by Bochkanov Sergey
        *************************************************************************/
        public static int nbcclserrorm(ref double[] b,
            ref double[,] xy,
            ref bool[,] m,
            int npoints)
        {
            int result = 0;
            int nvars = 0;
            int nclasses = 0;
            int i = 0;
            int j = 0;
            double[] workx = new double[0];
            double[] worky = new double[0];
            bool[] mx = new bool[0];
            int nmax = 0;
            int i_ = 0;

            System.Diagnostics.Debug.Assert((int)Math.Round(b[1]) == nbcvnum, "NBCClsErrorM: incorrect classificator array!");
            nvars = (int)Math.Round(b[2]);
            nclasses = (int)Math.Round(b[3]);
            workx = new double[nvars - 1 + 1];
            mx = new bool[nvars - 1 + 1];
            worky = new double[nclasses - 1 + 1];
            result = 0;
            for (i = 0; i <= npoints - 1; i++)
            {

                //
                // Process
                //
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    workx[i_] = xy[i, i_];
                }
                for (j = 0; j <= nvars - 1; j++)
                {
                    mx[j] = m[i, j];
                }
                nbcprocessm(ref b, ref workx, ref mx, ref worky);

                //
                // NBC version of the answer
                //
                nmax = 0;
                for (j = 0; j <= nclasses - 1; j++)
                {
                    if (worky[j] > worky[nmax])
                    {
                        nmax = j;
                    }
                }

                //
                // compare
                //
                if (nmax != (int)Math.Round(xy[i, nvars]))
                {
                    result = result + 1;
                }
            }
            return result;
        }


        /*************************************************************************
        Internal processing subroutine.
        *************************************************************************/
        private static void nbcprocessinternal(ref double[] b,
            ref double[] x,
            ref bool[] m,
            bool usem,
            ref double[] pc)
        {
            int i = 0;
            int j = 0;
            double s = 0;
            double v = 0;
            int xval = 0;
            int cur = 0;
            int curf = 0;
            int nvars = 0;
            int nclasses = 0;
            int vinfooffset = 0;
            int pinfooffset = 0;
            int cinfooffset = 0;
            bool bf = new bool();
            int l = 0;
            int half = 0;
            int first = 0;
            int middle = 0;
            int i_ = 0;

            System.Diagnostics.Debug.Assert((int)Math.Round(b[1]) == nbcvnum, "NBCProcess: incorrect classificator array!");
            nvars = (int)Math.Round(b[2]);
            nclasses = (int)Math.Round(b[3]);
            vinfooffset = (int)Math.Round(b[5]);
            pinfooffset = (int)Math.Round(b[6]);
            cinfooffset = (int)Math.Round(b[7]);
            for (j = 0; j <= nclasses - 1; j++)
            {
                pc[j] = b[pinfooffset + j];
            }
            cur = cinfooffset;
            curf = vinfooffset;
            for (i = 0; i <= nvars - 1; i++)
            {

                //
                // Missing value?
                //
                if (usem)
                {
                    bf = !m[i];
                }
                else
                {
                    bf = true;
                }

                //
                // Non-missing: calculate
                //
                if (bf)
                {

                    //
                    // Convert from real to index
                    //
                    if ((int)Math.Round(b[curf]) == 1)
                    {

                        //
                        // only one variable value is possible
                        //
                        xval = 0;
                    }
                    else
                    {

                        //
                        // use binary search to find which class value belongs to
                        //
                        l = (int)Math.Round(b[curf]) - 1;
                        first = curf + 1;
                        while (l > 0)
                        {
                            half = l / 2;
                            middle = first + half;
                            if (x[i] < b[middle])
                            {
                                l = half;
                            }
                            else
                            {
                                first = middle + 1;
                                l = l - half - 1;
                            }
                        }
                        xval = first - (curf + 1);
                    }

                    //
                    // Update
                    //
                    for (j = 0; j <= nclasses - 1; j++)
                    {
                        pc[j] = pc[j] * b[cur + xval * nclasses + j];
                    }

                    //
                    // Renormalize to avoid overflows
                    //
                    s = 0;
                    for (j = 0; j <= nclasses - 1; j++)
                    {
                        s = s + pc[j];
                    }
                    v = 1 / s;
                    for (i_ = 0; i_ <= nclasses - 1; i_++)
                    {
                        pc[i_] = v * pc[i_];
                    }
                }

                //
                // Update positions
                //
                cur = cur + (int)Math.Round(b[curf]) * nclasses;
                curf = curf + (int)Math.Round(b[curf]);
            }
        }
    }
}