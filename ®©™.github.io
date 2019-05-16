GitHub Help

Configuring a publishing source for GitHub Pages
GitHub Pages is available in public repositories with GitHub Free, and in public and private repositories with GitHub Pro, GitHub Team, GitHub Enterprise Cloud, and GitHub Enterprise Server. For more information, see "GitHub's products."

You can configure GitHub Pages to publish your site's source files from master, gh-pages, or a /docs folder on your master branch for Project Pages and other Pages sites that meet certain criteria.

If your site is a User or Organization Page that has a repository named <username>.github.ioor <orgname>.github.io, you cannot publish your site's source files from different locations. User and Organization Pages that have this type of repository name are only published from the master branch.

For more information about the different types of GitHub Pages sites, see "User, Organization, and Project Pages."

Default source settings for repositories without the username naming scheme
The default settings for publishing your site's source files depend on your site type and the branches you have in your site repository.

If your site repository doesn't have a master or gh-pages branch, your GitHub Pages publishing source is set to None and your site is not published.

none-source-setting

After you've created either a master or gh-pages branch, you can set one as your publishing source so that your site will be published.

If you fork or upload your site repository with only a master or gh-pages branch, your site's source setting will automatically be enabled for that branch.

Enabling GitHub Pages to publish your site from master or gh-pages
To select master or gh-pages as your publishing source, you must have the branch present in your repository. If you don't have a master or gh-pages branch, you can create them and then return to source settings to change your publishing source.

On GitHub, navigate to your GitHub Pages site's repository.

Under your repository name, click  Settings.

Repository settings button
Use the Select source drop-down menu to select master or gh-pages as your GitHub Pages publishing source.

select-gh-pages-or-master-as-source
Click Save.

click-save-next-to-source-selection
Publishing your GitHub Pages site from a /docs folder on your master branch
To publish your site's source files from a /docs folder on your master branch, you must have a master branch and your repository must:

have a /docs folder in the root of the repository
not follow the repository naming scheme <username>.github.ioor <orgname>.github.io
GitHub Pages will read everything to publish your site, including the CNAME file, from the /docs folder. For example, when you edit your custom domain through the GitHub Pages settings, the custom domain will write to /docs/CNAME.

Tip: If you remove the /docs folder from the master branch after it's enabled, your site won't build and you'll get a page build error message for a missing /docs folder.

On GitHub, navigate to your GitHub Pages site's repository.

Create a folder in the root of your repository on the master branch called /docs.

Under your repository name, click  Settings.

Repository settings button
Use the Select source drop-down menu to select master branch /docs folder as your GitHub Pages publishing source.

select-master-branch-docs-folder-as-source
Tip: The master branch /docs folder source setting will not appear as an option if the /docs folder doesn't exist on the master branch.

Click Save.

click-save-next-to-master-branch-docs-folder-source-selection
Further Reading
Viewing branches in your repository
Ask a human
Can't find what you're looking for?
Product
Features
Security
Enterprise
Case Studies
Pricing
Resources
Platform
Developer API
Partners
Atom
Electron
GitHub Desktop
Support
Help
Community Forum
Training
Status
Contact GitHub
Company
About
Blog
Careers
Press
Shop
© 2019 GitHub, Inc.
Terms
Privacy
