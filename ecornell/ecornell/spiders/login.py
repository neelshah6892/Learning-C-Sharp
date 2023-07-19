import scrapy


class LoginSpider(scrapy.Spider):
    name = "login"
    allowed_domains = ["ondemand.ecornell.com"]
    start_urls = ["https://ondemand.ecornell.com/"]

    def parse(self, response):
        inputs = response.css('form input')
        print(inputs)

        formdata = {}
        for input in inputs:
            name = input.css('::attr(type)').get()
            value = input.css('::attr(value)').get()
            formdata[name] = value

        formdata['username'] = 'cornell@hipointservices.com'
        formdata['password'] = '$anketShah81'

        return scrapy.FormRequest.from_response(
            response,
            formdata = formdata,
            callback = self.parse_after_login
        )

    def parse_after_login(self, response):
        print(response.xpath('.//div[@class = "col-md-4"]/p/a/text()').get())
