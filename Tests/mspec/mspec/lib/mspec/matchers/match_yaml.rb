class MatchYAMLMatcher

  def initialize(expected)
    if valid_yaml?(expected)
      @expected = expected
    else
      @expected = expected.to_yaml
    end
  end

  def matches?(actual)
    @actual = actual    
    clean_yaml(@actual) == @expected
  end

  def failure_message
    ["Expected #{@actual.inspect}", " to match #{@expected.inspect}"]
  end

  def negative_failure_message
    ["Expected #{@actual.inspect}", " to match #{@expected.inspect}"]
  end
  
  protected
  
  def clean_yaml(yaml)
    yaml.gsub(/([^-])\s+\n/, "\\1\n")
  end

  def valid_yaml?(obj)
    require 'yaml'
    begin
      YAML.load(obj)
    rescue
      false
    else
      true
    end
  end
end

class Object
  def match_yaml(expected)
    MatchYAMLMatcher.new(expected)
  end
end

