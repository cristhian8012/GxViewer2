namespace Genexus_Definition_HtmlBuilder
{
    public static class Styles
    {
        public const string Css = @"
* { box-sizing: border-box; }
body {
    font-family: Verdana, Arial, sans-serif;
    font-size: 11px;
    margin: 0; padding: 0;
    background: #d4d0c8;
    color: #000;
}
.FormBase { width: 100%; }
.iPageBody { padding: 12px; background: #d4d0c8; }

.PageTitleText {
    display: block;
    color: #cc0000;
    font-size: 14px;
    font-weight: bold;
    padding: 4px 0 10px 0;
    border-bottom: 1px solid #999;
    margin-bottom: 10px;
}

.iCategory { width: 100%; border-collapse: collapse; margin-bottom: 8px; }
.CategoryTitleText {
    display: block;
    background: #000;
    color: #fff;
    font-weight: bold;
    padding: 3px 6px;
    font-size: 11px;
}
.iCategoryBody { width: 100%; border-collapse: collapse; background: #ece9d8; }
.FormRow { height: 24px; }
.CaptionBack { background: #ece9d8; }
.FieldBack { background: #f0f3f5; padding: 4px 6px; }

.ConfirmationText { font-size: 11px; }
.OpText {
    display: inline-block;
    padding: 2px 10px;
    border: 1px solid #7f9db9;
    background: #f0f3f5;
    cursor: pointer;
    font-size: 11px;
    color: #000;
    text-decoration: none;
}
.OpText:hover { background: #e0e8f0; }

.iMessagesInnerArea {
    padding: 8px;
    color: #666;
    font-style: italic;
    background: #fff;
    border: 1px solid #ccc;
    margin: 4px;
}

.GridFilters { width: 100%; border-collapse: collapse; background: #e9e9e9; }
.GridFiltersForm { width: 100%; border-collapse: collapse; }
.FilterCaptionText {
    font-weight: bold;
    padding: 4px 8px;
    background: #e9e9e9;
    white-space: nowrap;
}
.FilterRow input,
.GridFiltersForm input[type=text],
.GridFiltersForm select {
    border: 1px solid #7f9db9;
    padding: 1px 3px;
    font-size: 11px;
    font-family: Verdana, Arial, sans-serif;
    background: #fff;
}
.FilterRow input { width: 100%; }
.DepBtn {
    display: inline-block;
    width: 18px; height: 18px;
    border: 1px solid #7f9db9;
    background: #f0f3f5;
    text-align: center;
    line-height: 16px;
    cursor: pointer;
    font-weight: bold;
    color: #036;
}
.DepLabel { color: #000; font-size: 11px; padding-left: 4px; }

.FilterBtnCell {
    background: #cfcfcf;
    vertical-align: top;
    padding: 6px 4px;
    width: 125px;
}
.FilterBtnCell .OpText { display: block; text-align: center; width: 100%; }

.Grid { border-collapse: collapse; width: 100%; font-size: 11px; background: #fff; }
.Grid th.GridTitle {
    background: #969696;
    color: #fff;
    font-weight: bold;
    padding: 4px 6px;
    border: 1px solid #7a7a7a;
    text-align: left;
    font-size: 11px;
}
.Grid td {
    padding: 3px 6px;
    border: 1px solid #d0d0d0;
    font-size: 11px;
}
.GridOdd { background: #ffffff; }
.GridEven { background: #f2f2f2; }
.Grid tr:hover td { background: #fff8c0; }

.GridPaging { width: 100%; }
.GridPaging td { padding: 3px; vertical-align: middle; }
.PagingInfo { font-family: Verdana; font-size: 10px; }

.GridOpsBar { background: #cfcfcf; text-align: center; padding: 6px 4px; }
.GridOpsBar .OpText { margin: 0 4px; }

.HiddenDataBar {
    margin-top: 10px;
    padding: 4px 6px;
    background: #cc0000;
    color: #fff;
    font-size: 10px;
    font-family: Verdana, Arial, sans-serif;
}
.HiddenDataBar label { margin-right: 6px; }
.HiddenDataBar .empty-event { font-weight: bold; margin-left: 6px; }

.GridFiltersLayout { width: 100%; border-collapse: collapse; }
.GridFiltersLayout > tbody > tr > td { vertical-align: top; }
";
    }
}