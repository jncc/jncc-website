
update-redirects
================

This tool updates the redirects in `src/JNCC.PublicWebsite/RewriteMaps.config` from the latest `redirect-list-new-website.xlsx` Excel spreadsheet.

Restore packages:

    dotnet restore

To run:

    dotnet run path/to/redirect-list-new-website.xlsx

It will also ensure that the resulting `RewriteMaps.config` is at least valid XML, but you must check the site works in pre-production, as other non-XML syntax errors can cause complete failure of the site at runtime!
