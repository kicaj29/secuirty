- [Thread modeling using Microsoft Threat Modeling Tool](#thread-modeling-using-microsoft-threat-modeling-tool)
  - [Application review](#application-review)
    - [Sample application review](#sample-application-review)
  - [Architecture review](#architecture-review)
  - [Data flow diagrams](#data-flow-diagrams)
  - [Templates](#templates)
- [Common security issues](#common-security-issues)
  - [XSS (Cross-site Scripting)](#xss-cross-site-scripting)
  - [CSRF (Cross-Site Request Forgery)](#csrf-cross-site-request-forgery)
- [resources](#resources)


# Thread modeling using Microsoft Threat Modeling Tool

## Application review

* users: who the users will be
* use cases: what the users are trying to do
* technology: tech. stack

### Sample application review

**Application for sharing documents between CompanyX and external client and partners:**

* users: user and roles include normal employees and also external client and partners - we do not know what kind of documents might be shared

* use case: securely share electronics documents with clients

* stack: Microsoft technology stack

## Architecture review 

![001-thread-modeling-arch-review.png](images/001-thread-modeling-arch-review.png)

## Data flow diagrams

![002-thread-modeling-dfd-level0.png](images/002-thread-modeling-dfd-level0.png)

![002-thread-modeling-dfd.png](images/002-thread-modeling-dfd.png)

## Templates

To create copy of template:

* go to create new template and copy author name
* close previous window and click open template
  * copy and rename template that you want based on (tb7 file)
  * next update section 
    ```xml
    <Manifest name="SDL TM Knowledge Base (Core)" id="cc62ebae-3748-431e-b1df-f4220dc9003f" version="4.1.0.11" author="TwC MSEC" />
    ```
    paste your user name into author field.

# Common security issues

## XSS (Cross-site Scripting)

https://www.youtube.com/watch?v=9kaihe5m3Lk

In case of XSS the attacker makes the victim’s browser execute a script (mostly JavaScript) that has been injected by the attacker while visiting a trusted website.

Example 1: the attacker visits a trusted website and types in a input value that is some kind of malicious JS code and this code is stored in DB. When normal user will open a page to display this data the malicious JS code will be executed.

Example 2: the attacker can send URL like this to a normal user: ```https://example.com/search?query=<script>alert(1)</script>```

Example 3: an attacher hacks one of used third party 

**Mitigation**: 

1. CSP (content security policy): to enable CSP, configure your web server to return an appropriate Content-Security-Policy HTTP header.   

   https://developers.google.com/web/fundamentals/security/csp   
   https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP   
   https://glebbahmutov.com/blog/disable-inline-javascript-for-security/   
   https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/default-src   
   https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/script-src   

   CSP can be set by HTTP server or in index.html using ```<meta>``` tag with an ```http-equiv```.

   CSP can ban inline script entirely and next we can add also exceptions using hash which inline scripts can be executed.

2. Using web framework that contains sanitization mechanism like angular: https://angular.io/guide/security#sanitization-and-security-contexts


## CSRF (Cross-Site Request Forgery)

https://www.youtube.com/watch?v=m0EHlfTgGUU

Cross-site request forgery, also known as one-click attack or session riding and abbreviated as CSRF or XSRF, is a type of malicious exploit of a website where unauthorized commands are submitted from a user that the web application trusts.

It uses mechanism of **automatic sending cookies** to the web site from which they were sent to the web browser. It is helpful because thx to this user does not have to type his password before every request but it also creates some security risks. Cookies are sent even if the request is made from other web-site!

**Example**: hacker creates a web site with e-commerce. You visit this web site because there are big discounts. When you click _buy button_ java-script code is executed that sends request to your bank API to transfer money to the hacker account... it works because in another tab you have open web site with you bank and the cookies are cached so they will be sent also in the request from the fake e-commerce web-site.

**Mitigation**:

1. SOP (same origin policy) and CORS
   
   SOP makes sure that that web-site A can send request to web-site B only if web-site B allows on this. By default it is not allowed.
   Thx to this **web browser will not allow** fake e-commerce web-site to send request to bank web-site because bank web-site by default blocks all other origins.

   https://developer.mozilla.org/en-US/docs/Web/Security/Same-origin_policy   
   https://www.securitylearn.net/tag/understanding-same-origin-policy/

   Sometimes we want do exception and then we use CORS.

   >**NOTE: "SOP does not restrict a site from loading JavaScript files from a different domain" (using ```<script>``` tag)**

2. Anti CSFR token

   https://cheatsheetseries.owasp.org/cheatsheets/Cross-Site_Request_Forgery_Prevention_Cheat_Sheet.html#CSRF_Prevention_without_a_Synchronizer_Token   
   https://developer.mozilla.org/en-US/docs/Web/Security/Same-origin_policy#cross-origin_network_access   
   https://www.netsparker.com/blog/web-security/protecting-website-using-anti-csrf-token/#:~:text=Anti%2DCSRF%20tokens%20(or%20simply,actions%20within%20a%20user's%20session.   

   **SOP controls requests that go via XMLHttpRequest (AJAX) or an ```<img>``` element.** Other requests are out of SOP scope! That`s why anti CSFR token is needed.

   Examples of resources which may be embedded cross-origin:
   * JavaScript with ```<script src="…"></script>```. Error details for    syntax errors are only available for same-origin scripts.
   It is related also with [JSONP](https://stackoverflow.com/a/2067584), other [link](https://www.w3schools.com/js/js_json_jsonp.asp#:~:text=JSONP%20stands%20for%20JSON%20with,instead%20of%20the%20XMLHttpRequest%20object.) about JSONP
   * CSS applied with ```<link rel="stylesheet" href="…">```. Due to the relaxed syntax rules of CSS, cross-origin CSS requires a correct Content-Type header. Restrictions vary by browser: Internet Explorer, Firefox, Chrome , Safari (scroll down to CVE-2010-0051) and Opera.
   * Images displayed by ```<img>```.
   * Media played by ```<video>``` and ```<audio>```.
   * External resources embedded with ```<object>``` and ```<embed>```.
   * Fonts applied with @font-face. Some browsers allow cross-origin fonts, others require same-origin.
   * Anything embedded by ```<iframe>```. Sites can use the X-Frame-Options header to prevent cross-origin framing.

   Anti CSFR token usually is a random value (token) generated by the server that is added as an hidden input filed. When the form is submitted then the server checks if the value is as expected, if not then the request is rejected. Thx to this even if the attacker will trigger somehow malicious request and the cookies will be automatically sent it will be rejected because the token will not match.

   For example:

   ```html
   <input name="__RequestVerificationToken" type="hidden" value="CfDJ8OT7CYZ8-OhOpWnspncR1scq-_jBE8XvN9CgBlkuk6KDPiNjkOuHznwfkOqY5e8P13fAy4hMEhigTuhXw5UOaNqNwlypiM3D1R1rXyty2Eco-vs7qMAzzEEK3KC0_chjVzOcN9gXu9paO3KiEjTN-6E">
   ```

# resources
https://app.pluralsight.com/library/courses/threat-modeling-with-microsoft-threat-modeling-tool-2016/table-of-contents   
https://docs.microsoft.com/en-us/azure/security/develop/threat-modeling-tool   
