﻿using System;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the QSL received date.
    /// </summary>
    public class QslRvcdDateTag : DateTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslRcvdDate;

        /// <summary>
        /// Creates a new QSLRDATE tag.
        /// </summary>
        public QslRvcdDateTag() { }

        /// <summary>
        /// Creates a new QSLRDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslRvcdDateTag(DateTime value) : base(value) { }

    }
}